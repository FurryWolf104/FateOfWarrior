using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite OpenSprite;
    public GameObject text;
    bool isOpened = false;
    bool isTarget = false;
    [SerializeField] bool isGoldenChest = false;
    public Player player;
    public List<GameObject> loot = new List<GameObject>();
    public Transform lootPlace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&& isTarget && !isOpened)
        {
            OpenChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isOpened)
        {

            isTarget = true;
           text.SetActive(true);
            player = collision.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isOpened)
        {
            isTarget = false;
            text.SetActive(false);
            player = null;
        }
    }

    public void OpenChest()
    {
        if (!isGoldenChest)
        {
            if (player != null && player.ironKeys > 0)
            {
                player.ironKeys--;
                player.openChestSound.Play();
                GameObject lootItem = Instantiate(loot[Random.Range(0, loot.Count)], lootPlace);
                lootItem.transform.parent = null;

                isOpened = true;
                text.SetActive(false);
                gameObject.GetComponent<SpriteRenderer>().sprite = OpenSprite;


            }
        }
        else
        {
            if (player != null && player.goldKeys > 0)
            {
                player.goldKeys--;
                player.openChestSound.Play();
                GameObject lootItem = Instantiate(loot[Random.Range(0, loot.Count)], lootPlace);
                lootItem.transform.parent = null;

                isOpened = true;
                text.SetActive(false);
                gameObject.GetComponent<SpriteRenderer>().sprite = OpenSprite;


            }
        }


    }
}
