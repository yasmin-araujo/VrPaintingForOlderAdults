using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonReader : MonoBehaviour
{
    [SerializeField]
    private GallerySO gallerySO;
    [SerializeField]
    private PalleteSO palleteSO;
    public int numDrawings = 0;

    private void Start() {
        LoadFromJson();
    }

    public void LoadFromJson()
    {
        string filePath = Application.dataPath + "/Scripts/Json/";
        string data = System.IO.File.ReadAllText(filePath + "Drawings.json");

        gallerySO.gallery = JsonConvert.DeserializeObject<Gallery>(data);
        numDrawings = gallerySO.gallery.drawings.Count;
        print("Drawings data loaded.");

        data = System.IO.File.ReadAllText(filePath + "Paints.json");
        palleteSO.pallete = JsonConvert.DeserializeObject<Pallete>(data);
        print("Paints data loaded.");
    }
}