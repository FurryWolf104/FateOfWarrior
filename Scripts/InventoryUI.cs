using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Player player;
    public List<GameObject> cells;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInventory();
    }
    public void UpdateInventory()
    {
        for (int i =0 ;i < player.items.Count;i++)
        {
            cells[i].SetActive(true);
            cells[i].transform.Find("Image").GetComponent<Image>().sprite = player.items[i].itemSprite;
            cells[i].transform.Find("ItemName").GetComponent<Text>().text = player.items[i].itemName;
            cells[i].transform.Find("Count").GetComponent<Text>().text = player.items[i].itemcount.ToString();
        }
    }
    public void InventoryOpen()
    {
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
