using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickedItem : MonoBehaviour
{
    [Header("Objects")]
    public Text itemNameBar;
    public Image itemSpriteBar;
    public Text descriptionBar;

    [Header("Values")]
    public float disappearanceSpeed = 0.10f;
    public float TimeTodisappearance =2;
    public bool isdisappearance = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Disappearance();
    }
    IEnumerator StartTimerDisappearance()
    {
        yield return new WaitForSeconds(TimeTodisappearance);
        isdisappearance = true;
        //gameObject.SetActive(false);

    }
    public void Disappearance()
    {
        if (isdisappearance)
        {
            itemNameBar.color = new Color(itemNameBar.color.r, itemNameBar.color.g, itemNameBar.color.b, itemNameBar.color.a - (disappearanceSpeed * Time.deltaTime));
            itemSpriteBar.color = new Color(itemNameBar.color.r, itemNameBar.color.g, itemNameBar.color.b, itemNameBar.color.a - (disappearanceSpeed * Time.deltaTime));
            descriptionBar.color = new Color(itemNameBar.color.r, itemNameBar.color.g, itemNameBar.color.b, itemNameBar.color.a - (disappearanceSpeed * Time.deltaTime));
        }
        if(itemNameBar.color.a<=0)
        {
            isdisappearance = false;
            gameObject.SetActive(false);
            DisappearanceRefresh();
        }
    }
    public void DisappearanceRefresh()
    {
        itemNameBar.color = new Color(itemNameBar.color.r, itemNameBar.color.g, itemNameBar.color.b, 1);
        itemSpriteBar.color = new Color(itemNameBar.color.r, itemNameBar.color.g, itemNameBar.color.b, 1);
        descriptionBar.color = new Color(itemNameBar.color.r, itemNameBar.color.g, itemNameBar.color.b, 1);
    }
    public void Picked(string itemName, Sprite itemSprite, string description)
    {
        StopAllCoroutines();
        isdisappearance = false;
        DisappearanceRefresh();
        itemNameBar.text = itemName;
        itemSpriteBar.sprite = itemSprite;
        descriptionBar.text = description;
        gameObject.SetActive(true);
        StartCoroutine(StartTimerDisappearance());

    }

}
