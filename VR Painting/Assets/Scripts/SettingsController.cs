using TMPro;
using UnityEditor;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private SettingsSO settingsSO;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;
    private TextMeshProUGUI brushText;
    private TextMeshProUGUI mainHandText;

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

            if (flag >= 2)
                break;
        }

        brushText.text = settingsSO.UseBrush ? "Ja" : "Nein";
        mainHandText.text = settingsSO.LeftHand ? "Links" : "Rechts";
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
}
