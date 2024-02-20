using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [Header("Сharacteristics")]
    [SerializeField] public float maxHealth = 100;
    [SerializeField] public float curHealth = 100;


    [Header("LVL Stats")]
    [SerializeField] public float expToLvl = 100;
    [SerializeField] public float curExp = 0;
    [SerializeField] public int level = 1;
    [SerializeField] public float gainExp; //Количество выпавшего опыта

    [Header("Attack Stats")]
    [SerializeField] public float damage = 15;

    [Range(0, 1f)] [SerializeField] public float armor = 0.05f;

    [Header("Possibility")] //Возможности существа
    [SerializeField] public bool canTakeDamage = true;
    [SerializeField] public bool canAttack = true;
    [SerializeField] public bool canMove = true;
    [SerializeField] public bool canJump = true;

    [Header("States")] //Состояния существа
    [SerializeField] public Side playerSide = Side.Right;
    [SerializeField] public bool isGrounded = true;
    [SerializeField] public bool isplayer = false;
    [SerializeField] public bool isStunned = false;
    [SerializeField] private Transform groundCheck;

    [Header("StatsForLvl")] //Повышение характеристик за уровень
    [SerializeField] public float maxHealthForLvl = 15;
    [SerializeField] public float expToLvlForLvl = 80;
    [SerializeField] public float damageForlvl = 5;
    [SerializeField] public float gainExpUpForLvl = 12;

    [Header("Loot")]
    public List<GameObject> lootList;
    [Range(0, 100f)] public List<float> lootChanceList;

    [Header("GroundCheck")]
    [SerializeField] LayerMask groundMask;

    [Header("Sounds")]
    [SerializeField] public AudioSource takeDamageAudio;



    public void TakeDamage(float damage) //Получение урона без отталкивания
    {
        if (canTakeDamage)
        {
            curHealth = curHealth - (damage-(armor*damage));
            if (takeDamageAudio != null) takeDamageAudio.Play();
        }
        
        if (curHealth <= 0) Death();
        
    }
    public void TakeDamageWithPush(float damage, float pushPower,Vector2 directionOfPush)
    {
        if (canTakeDamage)
        {
            curHealth = curHealth - (damage - (armor * damage));

            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            
            rigidbody2D.AddForce(directionOfPush * pushPower, ForceMode2D.Impulse);

            if (takeDamageAudio != null) takeDamageAudio.Play();
            StartCoroutine(Stun(0.4f));
            if (curHealth <= 0) Death();
        }
    }

    public enum Side //Перечисление сторон игрока
    {
        Right,
        Left
    }
    private void GroundCheck()
    {
        RaycastHit2D raycast2d = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.05f,groundMask);
       

        if (raycast2d.collider == null)
        {
            isGrounded = false;
        }
        else
        {
           // if (raycast2d.collider.tag == "Ground")
           // {
                isGrounded = true;
           // }
        }

        //Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.03f);
        //if (colliders.Length == 0)
        //{
        //    isGrounded = false;
        //}
        //else
        //{
        //    for (int i = 0; i < colliders.Length; i++)
        //    {
        //        if (colliders[i].gameObject.tag == "Ground")
        //        {
        //            isGrounded = true;
        //            break;
        //        }
        //    }
        //}
    }
    IEnumerator Stun(float timeOfStun)
    {
        isStunned = true;
        canMove = false;
        canJump = false;
        yield return new WaitForSeconds(timeOfStun);
        canMove = true;
        canJump = true;
        isStunned = false;

    }
   public void Death()
    {
        if (isplayer)
        {

            gameObject.SetActive(false);
            GameObject deathScreen = gameObject.transform.Find("DeathScreen").gameObject;
            deathScreen.transform.parent = null;
            deathScreen.SetActive(true);
            Time.timeScale = 0;


        }
        else
        {
            GiveExpToPlayer();
            Loot();
            Destroy(gameObject);
        }
    }

    public void GiveExpToPlayer()
    {
        GameObject player = GameObject.Find("Player");
        Actor playerActor = player.GetComponent<Actor>();
        playerActor.curExp += gainExp;
        
        if(playerActor.curExp >=playerActor.expToLvl )
        {
            playerActor.curExp -= playerActor.expToLvl;
            playerActor.LvlUp();

        }
    }
    public void LvlUp()
    {
        level++;
        StatsForLvlUp();
        //curExp = 0;


    }
    public void ChangeMoveAndJumpOff()
    {
        canMove = false;
        canJump = false;
    }
    public void ChangeMoveAndJumpOn()
    {
        if(!isStunned)
        canMove = true;
        canJump = true;
    }
    public void StatsForLvlUp()
    {
        if (isplayer)
        {
            Player player = GetComponent<Player>();
            maxHealth += maxHealthForLvl;
            expToLvl += expToLvlForLvl+(expToLvl/3);
            damage += damageForlvl;
            player.regenerationSpeed += player.regenerationSpeedForLvl;
        }
        else
        {
            maxHealth += maxHealthForLvl;
            gainExp += gainExpUpForLvl;
            damage += damageForlvl;
        }
    }
    private void Start()
    {
        if (!isplayer)
        {
            if (level > 1)
            {
                for (int i = 1; i < level; i++)
                {
                    StatsForLvlUp();
                }
            }
        }

        curHealth = maxHealth;
    }

    private void Update()
    {
        GroundCheck();
    }
    public void Loot()
    {
        for(int i =0;i<lootList.Count;i++)
        {
            if (Random.Range(0,100)<= lootChanceList[i])
            {
              GameObject lootItem = Instantiate(lootList[i],transform);
                lootItem.transform.parent = null;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
    }
}
