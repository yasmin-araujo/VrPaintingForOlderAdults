using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GallerySO : ScriptableObject
{
    public Gallery gallery;
}


[System.Serializable]
public class Gallery
{
    public List<Drawing> drawings = new List<Drawing>();
}

[System.Serializable]
public class Drawing
{
    public string id;
    public int level;
    public List<int> colors;
    public List<List<int>> matrix;
}