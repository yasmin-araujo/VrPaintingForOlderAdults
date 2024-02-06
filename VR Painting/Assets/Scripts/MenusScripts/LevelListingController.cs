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

    public void ListLevels(int difficulty)
    {
        ClearList();
        difficultyTitle.GetComponent<TextMeshProUGUI>().text = GetDifficultyName(difficulty);
        gallerySO.currentSelection.drawings = gallerySO.gallery.drawings.Where(drawing => drawing.level == difficulty).ToList();

        for (int i = 0; i < gallerySO.currentSelection.drawings.Count; i++)
        {
            Vector3 position = listViewport.GetComponent<Transform>().position;
            CreateButton(position, i, gallerySO.currentSelection.drawings[i].id);
        }
    }

    private void CreateButton(Vector3 position, int index, string spriteName)
    {
        GameObject newButton = Instantiate(levelPreviewPrefab, position, Quaternion.identity, listViewport.GetComponent<Transform>());
        Sprite sp = Resources.Load<Sprite>("Sprites/" + spriteName);
        newButton.GetComponent<Transform>().Find("Image").gameObject.GetComponent<Image>().sprite = sp;
        newButton.GetComponent<ToggleDeselect>().onValueChanged.AddListener((_) => GetComponent<MenuController>().LoadDrawing(index));
    }

    private string GetDifficultyName(int index)
    {
        switch (index)
        {
            case 0:
                return "Anfänger";
            case 1:
                return "Einfach";
            case 2:
                return "Normal";
            case 3:
                return "Schwer";
            case 4:
                return "Experte";
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
