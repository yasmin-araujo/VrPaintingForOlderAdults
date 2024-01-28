using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    public Action SetColorToBrush;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HandTag")
        {
            HandCollisionController handCol = other.gameObject.GetComponent<HandCollisionController>();
            SetColorToBrush();
        }
    }
}
