using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private IntSO selectedLevel;
    [SerializeField] private GallerySO gallerySO;
    [SerializeField] private PalleteSO palleteSO;
    [SerializeField] private GameObject board;
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
        if(drawingIndex == gallerySO.gallery.drawings.Count)
            return;
        
        board.GetComponent<BoardController>().LoadDrawing(gallerySO.gallery.drawings[drawingIndex]);
        
        drawingIndex++;
    }
}
