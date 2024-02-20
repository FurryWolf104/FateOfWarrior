using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFromItems : MonoBehaviour
{
    public GameObject player;
    public Actor actorPlayer;
    public Player playerClass;
    public void CrystalHeart()
    {
        actorPlayer = player.GetComponent<Actor>();
        actorPlayer.maxHealth += 25;
        actorPlayer.curHealth += 25;

    }
    public void RingOfPower()
    {
        actorPlayer = player.GetComponent<Actor>();
        actorPlayer.damage += 4;
    }
    public void bootsOfImpetuosity()
    {
        playerClass = player.GetComponent<Player>();
        playerClass.Speed += 1.5f;
    }
    public void ShardOfLife()
    {
        playerClass = player.GetComponent<Player>();
        playerClass.regenerationSpeed += 0.7f;
    }
    public void YalexsShield()
    {
        actorPlayer = player.GetComponent<Actor>();
        actorPlayer.armor += 0.07f;
    }
}
