using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject scrollViewItems;
    public GameObject itemPrefab;
    public float itemPositionY = -30;

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

            item.transform.SetParent(scrollViewItems.transform);
            itemPos.localScale = new Vector3(1, 1, 1);
            //itemPos.position = new Vector3(0, itemPositionY * i, 0);
            //itemPos.localPosition = new Vector3(0, itemPositionY * i, 0);
            //itemPos.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, itemPos.rect.height);
            itemPos.anchoredPosition = new Vector3(0, itemPositionY * i, 0);
            i++;
        }
    }

    public void ResetList()
    {
        RectTransform list = scrollViewItems.GetComponent<RectTransform>();
        list.sizeDelta = new Vector2(list.sizeDelta.x, 0);

        foreach (Transform child in scrollViewItems.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
