using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    private Material handsMaterial;
    private int paintColor;

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
