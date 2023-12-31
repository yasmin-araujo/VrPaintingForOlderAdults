using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using TMPro;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private GameObject pixelPrefab;
    [SerializeField] private PalleteSO palleteSO;

    private float boardHeight;
    private float boardWidth;
    private int progress = 0;

    public bool finished = false;

    public void LoadDrawing(Drawing drawing, List<Material> paintMaterials, Func<int> GetHandsColor)
    {
        ClearBoard();
        progress = 0;

        boardHeight = drawing.matrix.Count;
        boardWidth = drawing.matrix[0].Count;

        Vector3 posBoard = GetComponent<Transform>().position;
        float gameScale = GetComponent<Transform>().parent.gameObject.GetComponent<Transform>().localScale.x;
        float pixelScale = pixelPrefab.GetComponent<Transform>().localScale.x;

        for (float axisY = 0; axisY < boardHeight; axisY++)
        {
            for (float axisX = 0; axisX < boardWidth; axisX++)
            {
                int pixelColor = drawing.colors[drawing.matrix[(int)axisY][(int)axisX]];
                Vector3 position = new Vector3(posBoard.x + (axisX * pixelScale - ((float)boardWidth * pixelScale / 2)) * gameScale, posBoard.y + (axisY * pixelScale - ((float)boardHeight * pixelScale / 2)) * -gameScale, posBoard.z);
                CreatePixel((int)axisY, (int)axisX, position, pixelColor, drawing, paintMaterials[pixelColor], GetHandsColor);
            }
        }
        print("Drawing loaded");
    }

    private void CreatePixel(int row, int column, Vector3 position, int pixelColor, Drawing drawing, Material material, Func<int> GetHandsColor)
    {
        GameObject newPixel = Instantiate(pixelPrefab, position, Quaternion.identity, GetComponent<Transform>());
        PixelController pixCont = newPixel.GetComponent<PixelController>();
        pixCont.pixelColor = pixelColor;
        pixCont.row = row;
        pixCont.column = column;

        // Get color code
        string code = palleteSO.pallete.paints.Where(paint => paint.id == pixelColor).First().letter;

        // Set color placeholder
        Transform transform = newPixel.GetComponent<Transform>().Find("PixelVisual").gameObject.GetComponent<Transform>();
        GameObject colorNumberText = transform.Find("ColorNumber").gameObject;
        colorNumberText.GetComponent<TextMeshPro>().text = code;

        newPixel.GetComponent<InteractableUnityEventWrapper>().WhenSelect.AddListener(() =>
        {
            if (colorNumberText.GetComponent<TextMeshPro>().text != "")
            {
                progress++;
                newPixel.GetComponent<PixelController>().PaintPixel(material, GetHandsColor);
                if (progress >= boardHeight * boardWidth)
                {
                    finished = true;
                    print("FIM");
                }
            }
        });

        CheckBorders(row, column, drawing.matrix[row][column], transform, drawing.matrix);
    }

    private void CheckBorders(int row, int column, int originalPixelColor, Transform transform, List<List<int>> matrix)
    {
        if (row > 0 && matrix[row - 1][column] == originalPixelColor) // Top
        {
            transform.Find("TopBorder").gameObject.SetActive(false);
        }
        if (row < boardHeight - 1 && matrix[row + 1][column] == originalPixelColor) // Bottom
        {
            transform.Find("BottomBorder").gameObject.SetActive(false);
        }
        if (column > 0 && matrix[row][column - 1] == originalPixelColor) // Left
        {
            transform.Find("LeftBorder").gameObject.SetActive(false);
        }
        if (column < boardWidth - 1 && matrix[row][column + 1] == originalPixelColor) // Right
        {
            transform.Find("RightBorder").gameObject.SetActive(false);
        }
    }

    private void ClearBoard()
    {
        Transform transform = GetComponent<Transform>();
        foreach (Transform pixel in transform)
        {
            Destroy(pixel.gameObject);
        }
    }
}
