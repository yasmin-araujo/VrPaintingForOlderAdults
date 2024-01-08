using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PalleteController : MonoBehaviour
{
    [SerializeField] private PalleteSO palleteSO;
    [SerializeField] private GameObject paintPrefab;

    public void LoadPaints(List<int> paintsIndex, List<Material> paintMaterials, Action<Material, int> SetColorToBrush)
    {
        Vector3 posPallete = GetComponent<Transform>().position;
        // redo size
        float paintSize = 0.25F;//paintPrefab.GetComponent<RectTransform>().sizeDelta.x + 1;

        for (int i = 0; i < paintsIndex.Count; i++)
        {
            float posX = i * paintSize - (paintsIndex.Count - 1) * paintSize / 2;
            Vector3 position = new Vector3(posPallete.x + posX, posPallete.y, posPallete.z);
            string colorCode = palleteSO.pallete.paints.Where(paint => paint.id == paintsIndex[i]).First().letter;
            CreatePaint(position, paintMaterials[paintsIndex[i]], paintsIndex[i], colorCode, SetColorToBrush);
        }
    }

    private void CreatePaint(Vector3 position, Material material, int color, string colorCode, Action<Material, int> SetColorToBrush)
    {
        GameObject newPaint = Instantiate(paintPrefab, position, Quaternion.identity, GetComponent<Transform>());
        GameObject button = newPaint.GetComponent<Transform>().Find("Button").gameObject;
        GameObject buttonVisual = button.GetComponent<Transform>().Find("ButtonVisual").gameObject;
        print(buttonVisual.GetComponent<Transform>().Find("Button").gameObject.GetComponent<MeshRenderer>().materials[0]);
        buttonVisual.GetComponent<Transform>().Find("Button").gameObject.GetComponent<MeshRenderer>().material = material;
        button.GetComponent<InteractableUnityEventWrapper>().WhenSelect.AddListener(() => SetColorToBrush(material, color));

        TextMeshPro paintTextTMP = newPaint.GetComponent<Transform>().Find("ColorCode").gameObject.GetComponent<TextMeshPro>();
        paintTextTMP.text = colorCode;
        paintTextTMP.color = Color.black;
    }
}
