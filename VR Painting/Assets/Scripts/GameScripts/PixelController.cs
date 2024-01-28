using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PixelController : MonoBehaviour
{
    public int pixelColor;
    public int row, column;
    public bool useAssistance;

    public Action IncrementProgress;

    public void PaintPixel(Func<Material> GetHandsMaterial, Func<int> GetHandsColor)
    {
        // Pixel not elegible to be painted
        if (useAssistance && pixelColor != GetHandsColor())
            return;

        // Check if pixel already has the right color
        Transform pixelVisualTransform = GetComponent<Transform>().Find("PixelVisual").gameObject.GetComponent<Transform>();
        TextMeshPro pixelText = pixelVisualTransform.Find("ColorNumber").gameObject.GetComponent<TextMeshPro>();
        if (pixelText.text == "")
            return;

        if (pixelColor == GetHandsColor())
        {
            pixelText.text = "";
        }
        else if (GetHandsColor() == 0) // Black
        {
            pixelText.color = Color.white;
        }
        else 
        {
            pixelText.color = Color.black;
        }

        pixelVisualTransform.Find("TopBorder").gameObject.SetActive(false);
        pixelVisualTransform.Find("BottomBorder").gameObject.SetActive(false);
        pixelVisualTransform.Find("LeftBorder").gameObject.SetActive(false);
        pixelVisualTransform.Find("RightBorder").gameObject.SetActive(false);
        pixelVisualTransform.Find("Pixel").gameObject.GetComponent<Renderer>().material = GetHandsMaterial();
        IncrementProgress();
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
