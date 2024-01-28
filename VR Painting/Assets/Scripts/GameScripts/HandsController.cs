using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    [SerializeField] private GameObject leftBrush;
    [SerializeField] private GameObject rightBrush;
    [SerializeField] private GameObject leftFinger;
    [SerializeField] private GameObject rightFinger;
    [SerializeField] private SettingsSO settingsSO;
    public Material handsMaterial;
    public int paintColor;

    void Start()
    {
        rightFinger.SetActive(!settingsSO.UseBrush);
        leftFinger.SetActive(!settingsSO.UseBrush);
        rightBrush.SetActive(settingsSO.UseBrush && !settingsSO.LeftHand);
        leftBrush.SetActive(settingsSO.UseBrush && settingsSO.LeftHand);
    }

    public void InitializeHands(Material material, int color)
    {
        paintColor = color;
        handsMaterial = material;

        // Change hands color
        int flag = 0;
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "l_handMeshNode" || child.name == "r_handMeshNode")
            {
                flag++;
                child.gameObject.GetComponent<SkinnedMeshRenderer>().material = material;
                if (flag >= 2)
                    break;
            }
        }

    }
}
