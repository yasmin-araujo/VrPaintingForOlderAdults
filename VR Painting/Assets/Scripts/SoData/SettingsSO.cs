using UnityEngine;

[CreateAssetMenu]
public class SettingsSO : ScriptableObject
{
    [SerializeField] private bool _useBrush;
    [SerializeField] private bool _leftHand;

    public bool UseBrush
    {
        get { return _useBrush; }
        set { _useBrush = value; }
    }

    public bool LeftHand
    {
        get { return _leftHand; }
        set { _leftHand = value; }
    }
}