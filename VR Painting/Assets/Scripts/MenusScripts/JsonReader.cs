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

    private void Start() {
        LoadFromJson();
        // print(gallerySO.gallery.drawings.Count);
    }

    public void LoadFromJson()
    {
        string filePath = Application.dataPath + "/Scripts/Json/";
        
        string data = System.IO.File.ReadAllText(filePath + "Drawings.json");
        gallerySO.gallery = JsonConvert.DeserializeObject<Gallery>(data);
        print("Drawings data loaded.");

        data = System.IO.File.ReadAllText(filePath + "Paints.json");
        palleteSO.pallete = JsonConvert.DeserializeObject<Pallete>(data);
        print("Paints data loaded.");
    }
}