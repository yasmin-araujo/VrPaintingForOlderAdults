using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private SettingsSO settingsSO;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;
    private TextMeshPro brushText;

    void Start()
    {
        Transform[] children = SettingsMenu.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "BrushText")
            {
                brushText = child.gameObject.GetComponent<TextMeshPro>();
                break;
            }
        }
        brushText.text = settingsSO.useBrush ? "Deactivate" : "Activate";
    }

    public void SettingsButton(bool openSettings)
    {
        MainMenu.SetActive(!openSettings);
        SettingsMenu.SetActive(openSettings);
    }

    public void EnableBrush()
    {
        settingsSO.useBrush = !settingsSO.useBrush;
        brushText.text = settingsSO.useBrush ? "Deactivate" : "Activate";
    }
}
