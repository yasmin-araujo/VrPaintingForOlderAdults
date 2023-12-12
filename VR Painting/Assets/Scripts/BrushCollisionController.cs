using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushCollisionController : MonoBehaviour
{
    public bool hitPixel = false;

    private Material brushMaterial;
    private int paintColor;

    public void InitializeBrush(Material material, int color)
    {
        brushMaterial = material;
        paintColor = color;
        GetComponent<Renderer>().material = material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PixelTag")
        {
            hitPixel = true;
            other.gameObject.GetComponent<PixelController>().PaintPixel(paintColor, brushMaterial);
        }
        else if(other.gameObject.tag == "PaintTag")
        {
            other.gameObject.GetComponent<Button>().onClick.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PixelTag")
        {
            hitPixel = false;
        }
    }
}
