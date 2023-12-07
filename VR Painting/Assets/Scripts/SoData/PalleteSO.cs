using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PalleteSO : ScriptableObject
{
    public Pallete pallete;
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