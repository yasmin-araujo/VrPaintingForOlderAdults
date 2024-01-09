using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisionController : MonoBehaviour
{
    [SerializeField] private GameObject hands;

    public Material GetHandMaterial()
    {
        return hands.GetComponent<HandsController>().handsMaterial;
    }

    public int GetHandPaintColor()
    {
        return hands.GetComponent<HandsController>().paintColor;
    }
}
