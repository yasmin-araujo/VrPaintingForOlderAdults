using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PalleteController : MonoBehaviour
{
    [SerializeField] private PalleteSO palleteSO;
    [SerializeField] private List<Material> paintMaterials;
    [SerializeField] private GameObject paintPrefab;

    public void LoadPaints(List<int> paintsIndex)
    {
        Vector3 posPallete = GetComponent<Transform>().position;
        float paintSize = paintPrefab.GetComponent<RectTransform>().sizeDelta.x + 1;
        print("paint size: " + paintSize);

        for (int i = 0; i < paintsIndex.Count; i++)
        {
            float posX = i * paintSize - (paintsIndex.Count - 1) * paintSize / 2;
            Vector3 position = new Vector3(posPallete.x + posX, posPallete.y, posPallete.z);
            string colorCode = palleteSO.pallete.paints.Where(paint => paint.id == paintsIndex[i]).First().letter;
            CreatePaint(position, paintMaterials[paintsIndex[i]], colorCode);
        }
    }

    private void CreatePaint(Vector3 position, Material material, string colorCode)
    {
        GameObject newPaint = Instantiate(paintPrefab, position, Quaternion.identity, GetComponent<Transform>());
        newPaint.GetComponent<Image>().material = material;
        newPaint.GetComponent<Transform>().Find("ColorCode").gameObject.GetComponent<TextMeshProUGUI>().text = colorCode;
    }
}
