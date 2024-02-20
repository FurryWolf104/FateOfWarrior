using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestsSpawner : MonoBehaviour
{
    [Header("Points")]
    public List<GameObject> pointForSpawn;
    [Header("ChestsCount")]
    public int ironChestsCount;
    public int goldenChestsCount;
    [Header("ChestsPrefabs")]
    public GameObject ironChest;
    public GameObject goldenChest;
    // Start is called before the first frame update
    void Start()
    {
        List<bool> isChestOnPoint = new List<bool>();
        for(int i = 0; i < pointForSpawn.Count; i++)
        {
            isChestOnPoint.Add(false);
        }

        //for (int j = 0; j < ironChestsCount; j++)
        //{
        //    print(Random.Range(0, 100));
        //}
        while (goldenChestsCount > 0)
        {
            for (int i = 0; i < pointForSpawn.Count; i++)
            {
                if (isChestOnPoint[i])
                {
                    continue;
                }
                else
                {
                    int random = Random.Range(0, 100);
                    if (random < 10)
                    {
                        GameObject chest = Instantiate(goldenChest, pointForSpawn[i].transform);
                        chest.transform.parent = null;
                        goldenChestsCount--;
                        isChestOnPoint[i] = true;
                        break;

                    }
                }
            }
        }

        while (ironChestsCount>0)
        {
            for (int i = 0; i < pointForSpawn.Count;i++)
            {
                if (isChestOnPoint[i])
                {
                    continue;
                }
                else
                {
                    int random = Random.Range(0, 100);
                    if (random < 10)
                    {
                       GameObject chest = Instantiate(ironChest, pointForSpawn[i].transform);
                        chest.transform.parent = null;
                        ironChestsCount--;
                        isChestOnPoint[i] = true;
                        break;

                    }
                }
            }
        }
        //for (int i = 0; i < pointForSpawn.Count; i++)
        //{
        //    print(isChestOnPoint[i].ToString());
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
