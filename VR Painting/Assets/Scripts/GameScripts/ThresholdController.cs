using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThresholdController : MonoBehaviour
{
    [SerializeField] GameObject pixelPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HandTag")
        {
            HandCollisionController handCol = other.gameObject.GetComponent<HandCollisionController>();
            pixelPrefab.GetComponent<PixelController>().PaintPixel(handCol.GetHandMaterial, handCol.GetHandPaintColor, true);
        }
    }
}
