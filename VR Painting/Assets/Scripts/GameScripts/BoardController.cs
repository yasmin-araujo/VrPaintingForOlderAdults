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
    [SerializeField] private SettingsSO settingsSO;
    [SerializeField] private TrackerSO trackerSO;

    private float boardHeight;
    private float boardWidth;
    private int progress = 0;
    private Transform boardTransform;
    private Vector3 defaultPosition;
    [SerializeField] private Vector3 extraRotation;
    [SerializeField] private Vector3 extraPosition;

    public bool finished = false;

    void Start()
    {
        boardTransform = GetComponent<Transform>();
        defaultPosition = boardTransform.parent.position + new Vector3(0, 0, 0.1F);

        if (!settingsSO.UseTracking)
        {
            SetBoardOnDefaultPosition(true);
        }
    }

    public void LoadDrawing(Drawing drawing, Func<Material> GetHandsMaterial, Func<int> GetHandsColor)
    {
        ClearBoard();
        progress = 0;
        SetBoardOnDefaultPosition(false);

        boardHeight = drawing.matrix.Count;
        boardWidth = drawing.matrix[0].Count;

        Vector3 posBoard = boardTransform.position;
        float gameScale = boardTransform.parent.gameObject.GetComponent<Transform>().localScale.x;
        float pixelScale = pixelPrefab.GetComponent<Transform>().localScale.x;

        for (float axisY = 0; axisY < boardHeight; axisY++)
        {
            for (float axisX = 0; axisX < boardWidth; axisX++)
            {
                int pixelColor = drawing.colors[drawing.matrix[(int)axisY][(int)axisX]];
                Vector3 position = new Vector3(posBoard.x + (axisX * pixelScale - ((float)boardWidth * pixelScale / 2)) * gameScale, posBoard.y + (axisY * pixelScale - ((float)boardHeight * pixelScale)) * -gameScale, posBoard.z);
                CreatePixel((int)axisY, (int)axisX, position, pixelColor, drawing, GetHandsMaterial, GetHandsColor);
            }
        }

        boardTransform.position = trackerSO.trackerPosition;
        boardTransform.eulerAngles = trackerSO.trackerRotation;
        print("Drawing loaded");
    }

    private void CreatePixel(int row, int column, Vector3 position, int pixelColor, Drawing drawing, Func<Material> GetHandsMaterial, Func<int> GetHandsColor)
    {
        GameObject newPixel = Instantiate(pixelPrefab, position, Quaternion.identity, GetComponent<Transform>());
        PixelController pixCont = newPixel.GetComponent<PixelController>();
        pixCont.pixelColor = pixelColor;
        pixCont.row = row;
        pixCont.column = column;
        pixCont.useAssistance = settingsSO.UseAssistance;
        pixCont.IncrementProgress = () =>
        {
            progress++;
            if (progress >= boardHeight * boardWidth)
            {
                finished = true;
            }
        };

        // Get color code
        string code = palleteSO.pallete.paints.Where(paint => paint.id == pixelColor).First().letter;

        // Set color placeholder
        Transform transform = newPixel.GetComponent<Transform>().Find("PixelVisual").gameObject.GetComponent<Transform>();
        GameObject colorNumberText = transform.Find("ColorNumber").gameObject;
        colorNumberText.GetComponent<TextMeshPro>().text = code;

        // This would be used in case the poke interaction would be active
        // newPixel.GetComponent<InteractableUnityEventWrapper>().WhenSelect.AddListener(() =>
        // {
        //     if (colorNumberText.GetComponent<TextMeshPro>().text != "")
        //     {
        //         progress++;
        //         //newPixel.GetComponent<PixelController>().PaintPixel(GetHandsMaterial, GetHandsColor);
        //         if (progress >= boardHeight * boardWidth)
        //         {
        //             finished = true;
        //         }
        //     }
        // });

        CheckBorders(row, column, drawing.matrix[row][column], transform, drawing.matrix);
        GameObject pxlThreshold = transform.Find("Pixel").gameObject.GetComponent<Transform>().Find("PixelThreshold").gameObject;
        pxlThreshold.SetActive(!settingsSO.UseAssistance);
        float scale = settingsSO.thresholdSize * 0.4F + 0.2F;
        pxlThreshold.GetComponent<Transform>().localScale = new Vector3(scale, scale, 1);
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

    public void SetBoardOnDefaultPosition(bool saveValue)
    {
        boardTransform.position = defaultPosition;
        boardTransform.rotation = Quaternion.identity;
        if (saveValue)
        {
            trackerSO.trackerPosition = boardTransform.position;
            trackerSO.trackerRotation = boardTransform.eulerAngles;
        }
    }

    public void SetBoardPosition(GameObject gameObject)
    {
        boardTransform.position = gameObject.GetComponent<Transform>().position + extraPosition;
        boardTransform.eulerAngles = new Vector3(extraRotation.x, extraRotation.y, extraRotation.z);
        // boardTransform.eulerAngles = new Vector3(-gameObject.GetComponent<Transform>().eulerAngles.x + extraRotation.x, extraRotation.y, extraRotation.z);
        trackerSO.trackerPosition = boardTransform.position;
        trackerSO.trackerRotation = boardTransform.eulerAngles;
    }

    private void ClearBoard()
    {
        foreach (Transform pixel in boardTransform)
        {
            Destroy(pixel.gameObject);
        }
    }
}
