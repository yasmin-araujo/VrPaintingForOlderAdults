using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftBrush;
    [SerializeField] private GameObject rightBrush;
    [SerializeField] private bool useBrush;
    public Material handsMaterial;
    public int paintColor;

    void Start()
    {
        // leftBrush.SetActive(useBrush);
        rightBrush.SetActive(useBrush);
        leftHand.SetActive(!useBrush);
        rightHand.SetActive(!useBrush);
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
