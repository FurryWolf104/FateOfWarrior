using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportAltar : MonoBehaviour
{
    public Player player;
    public GameObject text;
    public Sprite finishTextSprite;
    public bool isActive = false;
    public bool isTarget = false;
    public bool isComplite = false;
    EnemySpawner EnemySpawner;
    Animator animatorController;

    [Header("PortalSettings")]
    static public int portalTime = 45;
    static public int curPortalTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        animatorController = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            isTarget = true;
            if(!isActive) text.SetActive(true);
            player = collision.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isTarget = false;
            text.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        ActivatePortal();
    }
    public void ActivatePortal()
    {

        if (Input.GetKey(KeyCode.E) && !isActive && !isComplite && isTarget)
        {
            EnemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
            EnemySpawner.spawnRate -= 4;
            EnemySpawner.curspawnCountPerLevelDifficulty += 1;
            isActive = true;
            text.SetActive(false);
            curPortalTime = 0;
            player.fonMusic.Stop();
            player.actionMusic.Play();
            animatorController.SetBool("isAction", true);
            print("Portal is activated");
            player.GetComponentInChildren<PlayerUIController>().PortalPanelState(true);
            StartCoroutine(TimerTick());
        }
        else if(Input.GetKey(KeyCode.E)&&isComplite && isTarget)
        {
            SaveStats.SaveStatsFromPlayer(player.gameObject);
            SaveStats.isSaved = true;
            DifficultyLevel.difLevel++;
            //SaveStats.SaveEnemySpawner = EnemySpawner;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }

   
    }
    IEnumerator TimerTick()
    {
        if(curPortalTime>=portalTime)
        {
            animatorController.SetBool("isAction", false);
            isActive = false;
            isComplite = true;
            player.fonMusic.Stop();
            player.actionMusic.Stop();
            player.musicBeforeAction.Play();
            curPortalTime = 0;
            text.GetComponent<SpriteRenderer>().sprite = finishTextSprite;
            EnemySpawner.spawnRate += 4;
            EnemySpawner.curspawnCountPerLevelDifficulty -= 1;
            EnemySpawner.spawnerIsActive = false;
            player.GetComponentInChildren<PlayerUIController>().PortalPanelState(false);
        }
        else
        {
            curPortalTime++;
            yield return new WaitForSeconds(1f);
            StartCoroutine(TimerTick());

        }
    }
}
