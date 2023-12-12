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
    [SerializeField] private GameObject brush;
    private int drawingIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        gallerySO.gallery.drawings = gallerySO.gallery.drawings.Where(drawing => drawing.level == selectedLevel.Value).ToList();
        print(gallerySO.gallery.drawings.Count);

        LoadNewGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadNewGame()
    {
        if (drawingIndex == gallerySO.gallery.drawings.Count)
            return;

        Drawing drawing = gallerySO.gallery.drawings[drawingIndex];
        board.GetComponent<BoardController>().LoadDrawing(drawing);
        pallete.GetComponent<PalleteController>().LoadPaints(drawing.colors, paintMaterials, (material, color) => brush.GetComponent<BrushCollisionController>().InitializeBrush(material, color));
        brush.GetComponent<BrushCollisionController>().InitializeBrush(paintMaterials[drawing.colors[0]], drawing.colors[0]);

        drawingIndex++;
    }
}
