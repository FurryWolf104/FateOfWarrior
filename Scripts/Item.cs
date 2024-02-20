using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Events;
public class Item : MonoBehaviour
{
    public GameObject player;
    public int itemId;
    public string itemName;
    public string description;
    public UnityEvent Effect;
    public SpriteRenderer spriteRenderer;
    Player playerClass;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Player")
        {
            
            player = collision.gameObject;  
            //player.GetComponent<Player>().items.Add(gameObject);

            AddToInventory(player);
            ShowPickedItem();
            GetComponent<EffectFromItems>().player = player;
            Effect.Invoke();
            playerClass.inventory.GetComponent<InventoryUI>().UpdateInventory();
            Destroy(gameObject);
        }
    }
    public void ShowPickedItem()
    {
        playerClass.pickedItem.GetComponent<PickedItem>().Picked(itemName, spriteRenderer.sprite, description);
    }
    public void AddToInventory(GameObject player)
    {

        bool DoesTheInventorHave = false;
        playerClass = player.GetComponent<Player>();
        playerClass.takeItemSound.Play();
        if (playerClass.items.Count != 0)
        {
            foreach (Player.ListOfItems listOfItems in playerClass.items)


                if (listOfItems.itemName == itemName)
                {
                    listOfItems.itemcount++;
                    DoesTheInventorHave = true;
                    break;
                }
        }

       


        //for (int i = 0; i < playerClass.items.Count; i++)
        //{
        //    if (itemName == playerClass.items[i].itemName)
        //    {
        //        playerClass.items[i].itemcount++;
        //        DoesTheInventorHave = true;
        //        break;
        //    }
        //}
        if (!DoesTheInventorHave)
        {
            playerClass.items.Add(new Player.ListOfItems(this.itemId,this.itemName, spriteRenderer.sprite));
        }
        
                
    }
}
