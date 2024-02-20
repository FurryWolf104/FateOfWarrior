using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevel : MonoBehaviour
{

   static public int difLevel = 1;
   static public int ChangeLevelTimer = 120;
   static public int timer = 0;


    private void Start()
    {
        
        StartCoroutine(TimerTick());
        
    }
    void Update()
    {
        //TimerTick();
    }
   static public void RefreshDifficulty()
    {
        
        difLevel = 1;
        ChangeLevelTimer = 120;
        timer = 0;

    }
    //public void TimerTick()
    //{
    //    //timer += Time.deltaTime;
    //    //if (timer >= ChangeLevelTimer)
    //    //{
    //    //    difLevel++;
    //    //    ChangeLevelTimer += 120;
    //    //}

    //}
    IEnumerator TimerTick()
    {
        timer++;
        if (timer >= ChangeLevelTimer)
        {
            difLevel++;
            ChangeLevelTimer += 120;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(TimerTick());
    }
    
}
