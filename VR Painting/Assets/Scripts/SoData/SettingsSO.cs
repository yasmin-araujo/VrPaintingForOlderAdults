using UnityEngine;

[CreateAssetMenu]
public class SettingsSO : ScriptableObject
{
    [SerializeField] private bool _useBrush;
    [SerializeField] private bool _leftHand;
    [SerializeField] private bool _useAssistance;

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
    
    public bool UseAssistance
    {
        get { return _useAssistance; }
        set { _useAssistance = value; }
    }
}