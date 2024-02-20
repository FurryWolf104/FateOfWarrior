using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Характеристики игрока
    //public static Player player;

    [Header("Options")]
    public float Speed = 3f;
    public float JumpForce = 10f;
    public float regenerationSpeed = 1f;
    public float regenerationSpeedForLvl = 0.5f;
    //public int jumpCount = 1;
    public int ironKeys = 0;
    public int goldKeys = 0;

    //[SerializeField] public bool isGrounded = true;
    //[SerializeField] private Side playerSide = Side.Right;
    //[SerializeField] private Transform groundCheck;
    private Transform transform;
    private Rigidbody2D rigidbody2D;
    private Animator animatorController;
    private Actor actor;

    [Header("Attacks Stats")]
    public GameObject attackPoint;
    public float startAttackSpeed;
    public float curAttackSpeed;
    public float attackRange;
    public LayerMask enemy;

    [Header("Inventory")]
    public GameObject pickedItem;
    public GameObject inventory;
    [SerializeField] public List<ListOfItems> items;
    [Header("Sounds")]
    public AudioSource attackSound;
    public AudioSource walkSound;
    public AudioSource openChestSound;
    public AudioSource takeKeySound;
    public AudioSource takeItemSound;
    [Header("Music")]
    public AudioSource fonMusic;
    public AudioSource actionMusic;
    public AudioSource musicBeforeAction;





    //public  void setPlayerState(bool state)
    //{
    //    isGrounded = state;
    //}
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
                rigidbody2D.velocity = new Vector2(1 * Speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
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
                rigidbody2D.velocity = new Vector2(1 * Speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
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
                rigidbody2D.velocity = new Vector2(-1 * Speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);

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
                rigidbody2D.velocity = new Vector2(-1 * Speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
            }
        }
    }

    public void Jump()
    {
        if (actor.canJump)
        {
            rigidbody2D.velocity = (Vector3.up * JumpForce * Time.fixedDeltaTime);
            //isGrounded = false;
            //jumpCount--;
        }
    }
    public void Attack()
    {
        if (curAttackSpeed <= 0 && actor.canAttack)
        {
            if (actor.isGrounded) gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            animatorController.Play("Knight_Attack");
            attackSound.Play();
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemy);
            if (actor.playerSide == Actor.Side.Right)
            {
                for (int i = 0; i < enemies.Length; i++)
                {

                    enemies[i].GetComponent<Actor>().TakeDamageWithPush(actor.damage, 1f, new Vector2(1, 1));
                }
            }
            else
            {
                for (int i = 0; i < enemies.Length; i++)
                {

                    enemies[i].GetComponent<Actor>().TakeDamageWithPush(actor.damage, 1f, new Vector2(-1, 1));
                }
            }
            curAttackSpeed = startAttackSpeed;

        }
        
        //StartCoroutine(attack());
    }
    public void Idle()
    {
        if (rigidbody2D.velocity == new Vector2(0,0))
            animatorController.SetBool("isRunning", false);
        //animatorController.Play("Knight_Idle");
    }


    IEnumerator attack()
    {
        
        animatorController.Play("Knight_Attack");
        yield return new WaitForSeconds(0.4f);
    }

    // Start is called before the first frame update
    void Start()
    {
        items = new List<ListOfItems>();
        transform = GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        actor = GetComponent<Actor>();
        LoadStats();

    }
    private void Update()
    {

        ReloadAttack();
        Regeneration();
    }
    public void ReloadAttack()
    {
        if (curAttackSpeed > 0) curAttackSpeed -= Time.deltaTime;
    }

        // Update is called once per frame
        void FixedUpdate()
    {

        //GroundCheck();
    }
    private void Regeneration()
    {
        if(actor.curHealth<=actor.maxHealth)
        {
            actor.curHealth += regenerationSpeed * Time.deltaTime;
        }
        if (actor.curHealth > actor.maxHealth) actor.curHealth = actor.maxHealth;
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
    public enum Side //Перечисление сторон игрока
    {
        Right,
        Left
    }
    
    [System.Serializable]
   public class ListOfItems
    {
        public int itemId;
        public string itemName;
        public Sprite itemSprite;
        public int itemcount;
        public ListOfItems(int itemId,string itemName, Sprite itemSprite)
        {
            this.itemId = itemId;
            this.itemName = itemName;
            this.itemSprite = itemSprite;
            this.itemcount = 1;
        }
    }
    //[ContextMenu("LoadStats")]
    public void LoadStats()
    {
        if (SaveStats.isSaved)
        {
            print("load");

            actor.maxHealth = SaveStats.maxHealth;
            actor.curHealth = actor.maxHealth;
            actor.armor = SaveStats.armor;
            actor.expToLvl = SaveStats.expToLvl;
            actor.curExp = SaveStats.curExp;
            actor.level = SaveStats.level;
            actor.damage = SaveStats.damage;
            Speed = SaveStats.Speed;
            JumpForce = SaveStats.JumpForce;
            regenerationSpeed = SaveStats.regenerationSpeed;
            ironKeys = SaveStats.ironKeys;
            goldKeys = SaveStats.goldKeys;
            items = SaveStats.items;
        }
        else print("SaveIsNull");
    }
    public void WalkSound()
    {
        walkSound.pitch = Random.Range(0.9f, 1.1f);
        walkSound.Play();
    }    
    //[ContextMenu("SaveStats")]
    //public void SavePlayer()
    //{
    //    SaveStats.player = gameObject;
    //}

}
