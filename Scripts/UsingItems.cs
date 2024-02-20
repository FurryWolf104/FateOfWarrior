using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UsingItems : MonoBehaviour
{
    public Player player;
    public UnityEvent effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            player = collision.gameObject.GetComponent<Player>();
            effect.Invoke();

            
            Destroy(gameObject);
        }
    }
    public void AddIronKey()
    {
        player.ironKeys++;
        player.takeKeySound.Play();
    }
    public void AddGoldenKey()
    {
        player.goldKeys++;
        player.takeKeySound.Play();
    }
}
