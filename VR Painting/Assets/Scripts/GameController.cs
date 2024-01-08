using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private IntSO selectedLevel;
    [SerializeField] private GallerySO gallerySO;
    [SerializeField] private List<Material> paintMaterials;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject pallete;
    [SerializeField] private GameObject hands;
    private int drawingIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        gallerySO.gallery.drawings = gallerySO.gallery.drawings.Where(drawing => drawing.level == selectedLevel.Value).ToList();
        print(gallerySO.gallery.drawings.Count);

        LoadNewGame();
    }

    private void LoadNewGame()
    {
        if (drawingIndex == gallerySO.gallery.drawings.Count)
            return;

        Drawing drawing = gallerySO.gallery.drawings[drawingIndex];
        board.GetComponent<BoardController>().LoadDrawing(drawing, paintMaterials);
        pallete.GetComponent<PalleteController>().LoadPaints(drawing.colors, paintMaterials, (material, color) => hands.GetComponent<HandsController>().InitializeHands(material, color));
        hands.GetComponent<HandsController>().InitializeHands(paintMaterials[drawing.colors[0]], drawing.colors[0]);

        drawingIndex++;
    }
}
