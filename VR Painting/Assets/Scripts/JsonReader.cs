using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonReader : MonoBehaviour
{
    public Gallery gallery = new Gallery();
    public Pallete pallete = new Pallete();
    public int numDrawings = 0;

    void Start()
    {
        LoadFromJson();
    }

    public void LoadFromJson()
    {
        string filePath = Application.dataPath + "/Scripts/";
        string data = System.IO.File.ReadAllText(filePath + "Drawings.json");

        gallery = JsonConvert.DeserializeObject<Gallery>(data);
        numDrawings = gallery.drawings.Count;
        print("Drawings data loaded.");

        data = System.IO.File.ReadAllText(filePath + "Paints.json");
        pallete = JsonConvert.DeserializeObject<Pallete>(data);
        print("Paints data loaded.");
    }
}

[System.Serializable]
public class Gallery
{
    public List<Drawing> drawings = new List<Drawing>();
}

[System.Serializable]
public class Drawing
{
    public int id;
    public int level;
    public List<int> colors;
    public List<List<int>> matrix;
}

[System.Serializable]
public class Pallete
{
    public List<Paint> paints = new List<Paint>();
}

[System.Serializable]
public class Paint
{
    public int id;
    public string name;
    public char letter;
}