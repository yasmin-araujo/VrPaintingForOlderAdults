using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private SettingsSO settingsSO;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;
    private TextMeshProUGUI brushText;
    private TextMeshProUGUI mainHandText;
    private TextMeshProUGUI assistanceText;
    private TextMeshProUGUI trackingText;
    private GameObject thresholdSizeSlider;

    void Start()
    {
        int flag = 0;
        Transform[] children = SettingsMenu.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "BrushStatusText")
            {
                brushText = child.gameObject.GetComponent<TextMeshProUGUI>();
                flag++;
            }
            else if (child.name == "MainHandText")
            {
                mainHandText = child.gameObject.GetComponent<TextMeshProUGUI>();
                flag++;
            }
            else if (child.name == "AssistanceStatusText")
            {
                assistanceText = child.gameObject.GetComponent<TextMeshProUGUI>();
                flag++;
            }
            else if (child.name == "TrackingStatusText")
            {
                trackingText = child.gameObject.GetComponent<TextMeshProUGUI>();
                flag++;
            }
            else if (child.name == "ThresholdSizeSlider")
            {
                thresholdSizeSlider = child.gameObject;
                flag++;
            }

            if (flag >= 5)
                break;
        }

        brushText.text = settingsSO.UseBrush ? "Ja" : "Nein";
        mainHandText.text = settingsSO.LeftHand ? "Links" : "Rechts";
        assistanceText.text = settingsSO.UseAssistance ? "Ja" : "Nein";
        trackingText.text = settingsSO.UseTracking ? "Ja" : "Nein";
        thresholdSizeSlider.GetComponent<Transform>().parent.gameObject.SetActive(!settingsSO.UseAssistance);
        thresholdSizeSlider.GetComponent<Slider>().value = settingsSO.thresholdSize;
    }

    public void SettingsButton(bool openSettings)
    {
        MainMenu.SetActive(!openSettings);
        SettingsMenu.SetActive(openSettings);
    }

    public void EnableBrush()
    {
        settingsSO.UseBrush = !settingsSO.UseBrush;
        brushText.text = settingsSO.UseBrush ? "Ja" : "Nein";
        EditorUtility.SetDirty(settingsSO);
    }

    public void ChangeMainHand()
    {
        settingsSO.LeftHand = !settingsSO.LeftHand;
        mainHandText.text = settingsSO.LeftHand ? "Links" : "Rechts";
        EditorUtility.SetDirty(settingsSO);
    }

    public void EnableAssistance(GameObject thresholdSize)
    {
        settingsSO.UseAssistance = !settingsSO.UseAssistance;
        assistanceText.text = settingsSO.UseAssistance ? "Ja" : "Nein";
        thresholdSize.SetActive(!settingsSO.UseAssistance);
        EditorUtility.SetDirty(settingsSO);
    }

    public void SetThresholdSize()
    {
        settingsSO.thresholdSize = thresholdSizeSlider.GetComponent<Slider>().value;
        EditorUtility.SetDirty(settingsSO);
    }

    public void EnableTracking()
    {
        settingsSO.UseTracking = !settingsSO.UseTracking;
        trackingText.text = settingsSO.UseTracking ? "Ja" : "Nein";
        EditorUtility.SetDirty(settingsSO);
    }
}
