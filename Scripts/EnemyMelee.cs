using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMelee : MonoBehaviour
{
    public float speed = 10;
    public GameObject player;
    public GameObject playerCheck;
    public LayerMask playerMask;
    Rigidbody2D rigidbody2D;
    public bool isPlayerOnMark; //Стоит ли игрок на метке
    public bool isPlayerOnArea; //Находится ли игрок в зоне видимости 
    public float visibleRange; //Дальность видимости


    //[Header("Patrol")]
    //public int StateWalk = 0;
    //bool isSideCanChanged = true;
    //public bool isPatroling;


    [Header("Attacks Stats")]
    public GameObject attackPoint;
    public float startAttackSpeed;
    public float curAttackSpeed;
    public float attackRange;


    private Actor actor;
    private Animator animatorController;
    [Header("Sounds")]
    public AudioSource attackSound;
    public AudioSource walkSound;
    void Start()
    {

        actor = GetComponent<Actor>();
        animatorController = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            FindPlayerObject();
        }
    }
    private void FindPlayerObject()
    {
        player = GameObject.Find("Player");
    }
    private void FindPlayerInMark()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(playerCheck.transform.position, 0.03f, playerMask);
        if (playerCollider == null)
        {
            isPlayerOnMark = false;
        }
        else
        {
            isPlayerOnMark = true;
            Attack();
        }
    }
    private void FindPlayerInArea()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, visibleRange, playerMask);
        if (playerCollider == null)
        {
            isPlayerOnArea = false;

            //if (canPatrol)
            //StartCoroutine(RandomByTime());
            //StartCoroutine(Patrol());
        }
        else 
        {
            StopAllCoroutines();
            isPlayerOnArea = true;
            //isPatroling=false;
        }
    }
    private void MoveToPlayer()
    {
        if (!isPlayerOnMark && isPlayerOnArea)
        {
            if (player.transform.position.x < transform.position.x)
            {
                MoveLeft();
            }
            else if (player.transform.position.x > transform.position.x)
            {
                MoveRight();
            }
        }
    }


    public void MoveRight()
    {
        if (actor.canMove)
        {
            if (actor.isGrounded != false)
            {

                if (actor.playerSide == Actor.Side.Left)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    actor.playerSide = Actor.Side.Right;
                }
                rigidbody2D.velocity = new Vector2(1 * speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
                //animatorController.Play("Knight_Move");
                animatorController.SetBool("isRunning", true);
            }
            else
            {
                if (actor.playerSide == Actor.Side.Left)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    actor.playerSide = Actor.Side.Right;
                }
                rigidbody2D.velocity = new Vector2(1 * speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
            }
        }

    }

    public void MoveLeft()
    {
        if (actor.canMove)
        {
            if (actor.isGrounded != false)
            {

                if (actor.playerSide == Actor.Side.Right)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    actor.playerSide = Actor.Side.Left;
                }
                rigidbody2D.velocity = new Vector2(-1 * speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);

                //animatorController.Play("Knight_Move");
                animatorController.SetBool("isRunning", true);
            }
            else
            {
                if (actor.playerSide == Actor.Side.Right)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    actor.playerSide = Actor.Side.Left;
                }
                rigidbody2D.velocity = new Vector2(-1 * speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
            }
        }
    }
    public void Attack()
    {
        if (curAttackSpeed <= 0 && actor.canAttack)
        {

            animatorController.Play("Attack");
            attackSound.Play();
            Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, playerMask);
            if (actor.playerSide == Actor.Side.Right)
            {

                for (int i = 0; i < players.Length; i++)
                {
                    player.GetComponent<Actor>().TakeDamage(actor.damage);
                }
            }
            else
            {
                for (int i = 0; i < players.Length; i++)
                {
                    player.GetComponent<Actor>().TakeDamage(actor.damage);
                }
            }
            curAttackSpeed = startAttackSpeed;

        }

        //StartCoroutine(attack());
    }
    // Update is called once per frame
    void Update()
    {
        FindPlayerInArea();
        FindPlayerInMark();
        ReloadAttack();
        Idle();



    }
    private void FixedUpdate()
    {
        MoveToPlayer();
    }
    //fix
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerCheck.transform.position, 0.03f);
    }
    public void ReloadAttack()
    {
        if (curAttackSpeed > 0) curAttackSpeed -= Time.deltaTime;

    }
    public void Idle()
    {
        if (rigidbody2D.velocity == new Vector2(0, 0))
            animatorController.SetBool("isRunning", false);
        //animatorController.Play("Knight_Idle");
    }
    public void WalkSound()
    {
        walkSound.pitch = Random.Range(0.9f, 1.1f);
        walkSound.Play();
    }

    //IEnumerator Patrol()
    //{
    //    yield return new WaitForSeconds(1f);

    //    bool patrolmove = true;
    //        if (patrolmove)
    //        {

    //        if (StateWalk == 0)
    //            {
    //                MoveRight();
    //            }
    //            else
    //            if (StateWalk == 1)
    //            {
    //                MoveLeft();
    //            }

    //             if (StateWalk>1)
    //            {
    //            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
    //            }

    //            yield return new WaitForSeconds(1f);
    //            patrolmove = false;
    //        yield return new WaitForSeconds(1f);
    //    }

    //}
    //IEnumerator RandomByTime()
    //{
    //    if (isSideCanChanged)
    //    {
    //        StateWalk = Random.Range(0, 4);
    //        isSideCanChanged = false;
    //        yield return new WaitForSeconds(2f);
    //        isSideCanChanged = true;
    //    }

    //}


}
