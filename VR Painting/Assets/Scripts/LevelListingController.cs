using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelListingController : MonoBehaviour
{
    [SerializeField] private GallerySO gallerySO;
    [SerializeField] private GameObject levelPreviewPrefab;
    [SerializeField] private GameObject difficultyTitle;
    [SerializeField] private GameObject listViewport;
    [SerializeField] private Sprite sprite;

    void Start()
    {

    }

    public void ListLevels(int difficulty)
    {
        ClearList();
        difficultyTitle.GetComponent<TextMeshPro>().text = GetDifficultyName(difficulty);
        gallerySO.gallery.drawings = gallerySO.gallery.drawings.Where(drawing => drawing.level == difficulty).ToList();

        for (int i = 0; i < gallerySO.gallery.drawings.Count; i++)
        {
            Vector3 position = new Vector3(0, 0, 0);
            CreateButton(position, i);
        }
    }

    private void CreateButton(Vector3 position, int index)
    {
        GameObject newButton = Instantiate(levelPreviewPrefab, position, Quaternion.identity, listViewport.GetComponent<Transform>());
        newButton.GetComponent<Transform>().Find("Image").gameObject.GetComponent<Image>().sprite = sprite;
        newButton.GetComponent<ToggleDeselect>().onValueChanged.AddListener((_) => GetComponent<MenuController>().LoadDrawing(index));
    }

    private string GetDifficultyName(int index)
    {
        switch (index)
        {
            case 0:
                return "Einfach";
            case 1:
                return "Normal";
            case 2:
                return "Schwer";
            default:
                return "Error";
        }
    }

    private void ClearList()
    {
        Transform transform = listViewport.GetComponent<Transform>();
        foreach (Transform level in transform)
        {
            Destroy(level.gameObject);
        }
    }
}
