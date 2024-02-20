using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveLine : MonoBehaviour
{
   public GameObject Line;
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
           Line.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Line.SetActive(false);
    }
}
