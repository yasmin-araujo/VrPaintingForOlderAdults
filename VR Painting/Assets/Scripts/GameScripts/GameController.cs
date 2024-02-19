using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private IntSO drawingIndex;
    [SerializeField] private GallerySO gallerySO;
    [SerializeField] private SettingsSO settingsSO;

    [Header("Game Objects References")]
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject pallete;
    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject vuforiaTracker;
    [SerializeField] private GameObject nextMenu;

    [Header("Paint Data")]
    [SerializeField] private List<Material> paintMaterials;

    private MetricsController metricsController;

    // Start is called before the first frame update
    void Start()
    {
        LoadNewGame();
        vuforiaTracker.SetActive(settingsSO.UseTracking);
        metricsController = GetComponent<MetricsController>();
    }

    void Update()
    {
        if (board.GetComponent<BoardController>().finished)
        {
            board.GetComponent<BoardController>().finished = false;
            ShowNextDrawingMenu();
            metricsController.WriteCSV();
        }
    }

    public void ShowNextDrawingMenu()
    {
        nextMenu.SetActive(true);
        board.SetActive(false);
        int flag = 0;
        Transform[] children = nextMenu.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "NextDrawing")
            {
                string drawingName = gallerySO.currentSelection.drawings[drawingIndex.Value % gallerySO.currentSelection.drawings.Count].id;
                child.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + drawingName);
                flag++;
            }
            else if (child.name == "FinishedDrawing")
            {
                string drawingName = gallerySO.currentSelection.drawings[(drawingIndex.Value - 1 >= 0 ? drawingIndex.Value : gallerySO.currentSelection.drawings.Count) - 1].id;
                child.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + drawingName);
                flag++;
            }

            if (flag >= 2)
                break;
        }
    }

    public void GoToNextDrawing()
    {
        nextMenu.SetActive(false);
        board.SetActive(true);
        LoadNewGame();
    }

    private void LoadNewGame()
    {
        if (gallerySO.currentSelection.drawings.Count == 0)
            return;

        if (drawingIndex.Value == gallerySO.currentSelection.drawings.Count)
            drawingIndex.Value = 0;

        Drawing drawing = gallerySO.currentSelection.drawings[drawingIndex.Value];
        board.GetComponent<BoardController>().LoadDrawing(drawing, () => hands.GetComponent<HandsController>().handsMaterial,
                                                            () => hands.GetComponent<HandsController>().paintColor);
        pallete.GetComponent<PalleteController>().LoadPaints(drawing.colors, paintMaterials, (material, color) => SetColorToBrush(material, color), !settingsSO.UseBrush);
        SetColorToBrush(paintMaterials[drawing.colors[0]], drawing.colors[0]);

        metricsController.UpdateCurrentDrawing(drawing.id);

        drawingIndex.Value++;
    }

    private void SetColorToBrush(Material material, int color)
    {
        hands.GetComponent<HandsController>().InitializeHands(material, color);

        Transform[] children = board.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject.tag == "PixelTag")
                child.gameObject.GetComponent<PixelController>().HighlightPixelsFromColor(material, color);
        }
    }
}
