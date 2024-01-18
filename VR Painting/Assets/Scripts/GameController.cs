using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // [SerializeField] private IntSO selectedLevel;
    [SerializeField] private IntSO drawingIndex;
    [SerializeField] private GallerySO gallerySO;
    [SerializeField] private SettingsSO settingsSO;
    [SerializeField] private List<Material> paintMaterials;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject pallete;
    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject nextMenu;

    // Start is called before the first frame update
    void Start()
    {
        // gallerySO.gallery.drawings = gallerySO.gallery.drawings.Where(drawing => drawing.level == selectedLevel.Value).ToList();
        // print(gallerySO.gallery.drawings.Count);

        LoadNewGame();
    }

    void Update()
    {
        if (board.GetComponent<BoardController>().finished)
        {
            board.GetComponent<BoardController>().finished = false;
            nextMenu.SetActive(true);
        }
    }

    public void NextDrawing()
    {
        nextMenu.SetActive(false);
        LoadNewGame();
    }

    private void LoadNewGame()
    {
        if (drawingIndex.Value == gallerySO.currentSelection.drawings.Count)
            drawingIndex.Value = 0;

        Drawing drawing = gallerySO.currentSelection.drawings[drawingIndex.Value];
        board.GetComponent<BoardController>().LoadDrawing(drawing, () => hands.GetComponent<HandsController>().handsMaterial, () => hands.GetComponent<HandsController>().paintColor);
        pallete.GetComponent<PalleteController>().LoadPaints(drawing.colors, paintMaterials, (material, color) => SetColorToBrush(material, color));
        SetColorToBrush(paintMaterials[drawing.colors[0]], drawing.colors[0]);

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
