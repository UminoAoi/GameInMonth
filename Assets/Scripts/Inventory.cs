using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject scrollViewItems;
    public GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void PressMenuButton()
    {
        Time.timeScale = 0;
        MakeInventoryList();
        gameObject.SetActive(true);

    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void MakeInventoryList()
    {
        ResetList();

        int i = 0;

        foreach (KeyValuePair<CollectableNames, int> pair in PlayerStateManager.GetInventory())
        {
            GameObject item = Instantiate(itemPrefab);
            List<TextMeshProUGUI> tmChildren = new List<TextMeshProUGUI>();

            foreach (Transform child in item.transform)
            {
                tmChildren.Add(child.GetComponent<TextMeshProUGUI>());
            }

            RectTransform list = scrollViewItems.GetComponent<RectTransform>();
            list.sizeDelta = new Vector2(list.sizeDelta.x, list.sizeDelta.y + 30);

            tmChildren[0].text = pair.Key.ToString();
            tmChildren[1].text = pair.Value.ToString();

            RectTransform itemPos = item.GetComponent<RectTransform>();
            itemPos.position = new Vector2(5, -30 * i);

            item.transform.SetParent(scrollViewItems.transform); //coś z rozmiarem jest nie tak
            i++;
        }
    }

    public void ResetList()
    {
        foreach (Transform child in scrollViewItems.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
