using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SettingsSO : ScriptableObject
{
    [SerializeField]
    private bool _useBrush;

    public bool UseBrush
    {
        get { return _useBrush; }
        set { _useBrush = value; }
    }
}