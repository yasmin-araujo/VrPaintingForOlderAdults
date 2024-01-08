using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PixelController : MonoBehaviour
{
    public int pixelColor;
    public int row, column;

    public void PaintPixel(Material material, Func<int> GetHandsColor)
    {
        if (pixelColor == GetHandsColor())
        {
            Transform pixelVisualTransform = GetComponent<Transform>().Find("PixelVisual").gameObject.GetComponent<Transform>();
            pixelVisualTransform.Find("Pixel").gameObject.GetComponent<Renderer>().material = material;
            pixelVisualTransform.Find("ColorNumber").gameObject.GetComponent<TextMeshPro>().text = "";
            pixelVisualTransform.Find("TopBorder").gameObject.SetActive(false);
            pixelVisualTransform.Find("BottomBorder").gameObject.SetActive(false);
            pixelVisualTransform.Find("LeftBorder").gameObject.SetActive(false);
            pixelVisualTransform.Find("RightBorder").gameObject.SetActive(false);
        }
    }

    private void HighlightPixelsFromColor(int color)
    {
        if (pixelColor == color)
        {
            TextMeshPro TMP = GetComponent<Transform>().Find("ColorNumber").gameObject.GetComponent<TextMeshPro>();
            TMP.fontSize = 8;
            TMP.fontStyle = FontStyles.Bold;
        }
        else
        {
            TextMeshPro TMP = GetComponent<Transform>().Find("ColorNumber").gameObject.GetComponent<TextMeshPro>();
            TMP.fontSize = 5;
            TMP.fontStyle = FontStyles.Normal;
        }
    }
}
