using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class SaveStats
{
    static public bool isSaved = false;
    [Header("Actor")]
    static public float maxHealth = 0;
    static public float curHealth = 0;


    [Header("LVL Stats")]
    static public float expToLvl = 0;
    static public float curExp = 0;
    static public int level = 0;
    static public float gainExp; //Количество Выпадающего опыта опыта

    [Header("Attack Stats")]
    static public float damage = 0;

    static public float armor = 0f;
    [Header("Player")]
    static public float Speed = 0;
    static public float JumpForce = 0f;
    static public float regenerationSpeed = 0f;
    static public float regenerationSpeedForLvl = 0f;
    static public int ironKeys = 0;
    static public int goldKeys = 0;
    public static List<Player.ListOfItems> items;


    [Header("EnemySpawner")]
    static public int difLevel = 0;
    static public int ChangeLevelTimer = 0;
    static public int timer = 0;

    public static void SaveStatsFromPlayer(GameObject player)
    {
            
            Actor loadActor = player.GetComponent<Actor>();
            Player loadPlayer = player.GetComponent<Player>();

            maxHealth = loadActor.maxHealth;
            expToLvl = loadActor.expToLvl;
            curExp = loadActor.curExp;
            level = loadActor.level;
            damage = loadActor.damage;
            armor = loadActor.armor;
            Speed = loadPlayer.Speed;
            JumpForce = loadPlayer.JumpForce;
            regenerationSpeed = loadPlayer.regenerationSpeed;
            ironKeys = loadPlayer.ironKeys;
            goldKeys = loadPlayer.goldKeys;
            items = loadPlayer.items;
    }
    public static void SaveEnemySpawner(EnemySpawner enemySpawner)
    {
        //difLevel = enemySpawner.dif
    }
}
