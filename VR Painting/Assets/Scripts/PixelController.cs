using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PixelController : MonoBehaviour
{
    public int pixelColor;
    public int row, column;

    public Action IncrementProgress;

    public void PaintPixel(Func<Material> GetHandsMaterial, Func<int> GetHandsColor)
    {
        if (pixelColor == GetHandsColor())
        {
            Transform pixelVisualTransform = GetComponent<Transform>().Find("PixelVisual").gameObject.GetComponent<Transform>();
            if (pixelVisualTransform.Find("ColorNumber").gameObject.GetComponent<TextMeshPro>().text == "")
                return;

            pixelVisualTransform.Find("Pixel").gameObject.GetComponent<Renderer>().material = GetHandsMaterial();
            pixelVisualTransform.Find("ColorNumber").gameObject.GetComponent<TextMeshPro>().text = "";
            pixelVisualTransform.Find("TopBorder").gameObject.SetActive(false);
            pixelVisualTransform.Find("BottomBorder").gameObject.SetActive(false);
            pixelVisualTransform.Find("LeftBorder").gameObject.SetActive(false);
            pixelVisualTransform.Find("RightBorder").gameObject.SetActive(false);
            IncrementProgress();
        }
    }

    public void HighlightPixelsFromColor(Material material, int color)
    {
        TextMeshPro TMP = GetComponent<Transform>().Find("PixelVisual").gameObject.GetComponent<Transform>().Find("ColorNumber").gameObject.GetComponent<TextMeshPro>();
        if (pixelColor == color)
        {
            TMP.fontSize = 6.5F;
            TMP.fontStyle = FontStyles.Bold;
            TMP.color = material.GetColor("_Color");
        }
        else
        {
            TMP.fontSize = 5;
            TMP.fontStyle = FontStyles.Normal;
            TMP.color = Color.black;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HandTag")
        {
            HandCollisionController handCol = other.gameObject.GetComponent<HandCollisionController>();
            PaintPixel(handCol.GetHandMaterial, handCol.GetHandPaintColor);
        }
    }
}
