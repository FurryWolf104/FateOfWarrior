using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public Player player;
    Actor actor;
    public InventoryUI playerInventoryUI;
    public Pause pause;


    // Start is called before the first frame update
    void Start()
    {
        if (player!= null)
        {
            player = GetComponent<Player>();
            actor = GetComponent<Actor>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (player.isGrounded)
        //{
            if (Input.GetKey(KeyCode.RightArrow))
            {
                player.MoveRight();
            }
            else
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                player.MoveLeft();
            }
            //else
            //{
            //    player.Idle();
            //}

        



       // }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    player.Jump();
        //}
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            player.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
           playerInventoryUI.InventoryOpen();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pause.isPaused) pause.StartPause();
            else pause.ClosePause();
        }

        player.Idle();

        if (actor.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Jump();
            }
        }
    }
    
    
}
