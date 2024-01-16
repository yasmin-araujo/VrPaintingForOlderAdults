using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private IntSO selectedLevel;
    [SerializeField] private GallerySO gallerySO;
    [SerializeField] private SettingsSO settingsSO;
    [SerializeField] private List<Material> paintMaterials;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject pallete;
    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject innerCylinder;
    private int drawingIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        gallerySO.gallery.drawings = gallerySO.gallery.drawings.Where(drawing => drawing.level == selectedLevel.Value).ToList();
        print(gallerySO.gallery.drawings.Count);

        LoadNewGame();
    }

    void Update()
    {
        if (board.GetComponent<BoardController>().finished)
        {
            board.GetComponent<BoardController>().finished = false;
            innerCylinder.SetActive(true);
        }
    }

    public void NextDrawing()
    {
        innerCylinder.SetActive(false);
        LoadNewGame();
    }

    private void LoadNewGame()
    {
        if (drawingIndex == gallerySO.gallery.drawings.Count)
            drawingIndex = 0;

        Drawing drawing = gallerySO.gallery.drawings[drawingIndex];
        board.GetComponent<BoardController>().LoadDrawing(drawing, () => hands.GetComponent<HandsController>().handsMaterial, () => hands.GetComponent<HandsController>().paintColor);
        pallete.GetComponent<PalleteController>().LoadPaints(drawing.colors, paintMaterials, (material, color) => hands.GetComponent<HandsController>().InitializeHands(material, color));
        hands.GetComponent<HandsController>().InitializeHands(paintMaterials[drawing.colors[0]], drawing.colors[0]);

        drawingIndex++;
    }
}
