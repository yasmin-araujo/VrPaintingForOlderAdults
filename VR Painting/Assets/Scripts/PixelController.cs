using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PixelController : MonoBehaviour
{
    public int pixelColor;
    public int row, column;

    private void PaintPixel(int color, Material material)
    {
        if (pixelColor == color)
        {
            GetComponent<Renderer>().material = material;
            Transform transform = GetComponent<Transform>();
            transform.Find("ColorNumber").gameObject.GetComponent<TextMeshPro>().text = "";
            transform.Find("TopBorder").gameObject.SetActive(false);
            transform.Find("BottomBorder").gameObject.SetActive(false);
            transform.Find("LeftBorder").gameObject.SetActive(false);
            transform.Find("RightBorder").gameObject.SetActive(false);
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
