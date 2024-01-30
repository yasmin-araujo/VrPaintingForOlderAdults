using UnityEngine;

[CreateAssetMenu]
public class SettingsSO : ScriptableObject
{
    [SerializeField] private bool _useBrush;
    [SerializeField] private bool _leftHand;
    [SerializeField] private bool _useAssistance;
    [SerializeField] private bool _useTracking;
    public float assistanceIntensity;

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

    public bool UseTracking
    {
        get { return _useTracking; }
        set { _useTracking = value; }
    }
}