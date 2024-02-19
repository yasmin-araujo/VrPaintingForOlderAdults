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
    private int currentColor = -1;

    public Action IncrementProgress;

    public Action IncrementMissesMetric;

    public void PaintPixel(Func<Material> GetHandsMaterial, Func<int> GetHandsColor, bool fromThreshold)
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
            pixelVisualTransform.Find("TopBorder").gameObject.SetActive(false);
            pixelVisualTransform.Find("BottomBorder").gameObject.SetActive(false);
            pixelVisualTransform.Find("LeftBorder").gameObject.SetActive(false);
            pixelVisualTransform.Find("RightBorder").gameObject.SetActive(false);
            IncrementProgress();
        }
        else if (!fromThreshold)
        {
            // When the assistance is off and the color is wrong, the function has to be called from the 
            // ThresholdController in order to paint the pixel. This allows us to define a threshold
            // when painting the wrong pixels.
            return;
        }
        else
        {
            IncrementMissesMetric();
            // Ensures contrast in case pixel will be painted with black
            pixelText.color = GetHandsColor() == 0 ? Color.white : Color.black;
        }

        pixelVisualTransform.Find("Pixel").gameObject.GetComponent<Renderer>().material = GetHandsMaterial();
        currentColor = GetHandsColor();
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
            // Ensures there's contrast in case pixel is colored with black
            TMP.color = currentColor == 0 ? Color.white : Color.black;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HandTag")
        {
            HandCollisionController handCol = other.gameObject.GetComponent<HandCollisionController>();
            PaintPixel(handCol.GetHandMaterial, handCol.GetHandPaintColor, false);
        }
    }
}
