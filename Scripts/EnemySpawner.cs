using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Line> lines;
    public List<Enemy> enemies;
    public float spawnRate = 5;
    public float curSpawnRate = 1;
    public float spawnCount = 0;
    //public float spawnCountPerLevelDifficulty = 0.3f;
    public float curspawnCountPerLevelDifficulty = 0.3f;
    public bool spawnerIsActive= false;
    public Line activeLine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Line line in lines)
        {
            if (line.Point.activeSelf)
            {
                activeLine = line;
                break;
            }
        }
        SpawnRate();

    }



    public void SpawnRate()
    {
        if (spawnerIsActive)
        {
            if (curSpawnRate <= 0)
            {
                spawnCount += 1 + (curspawnCountPerLevelDifficulty * DifficultyLevel.difLevel);

                while (spawnCount > 0)
                {


                    GameObject randomPlace = new GameObject();
                    randomPlace.transform.position = new Vector3(Random.Range(activeLine.Point.transform.position.x - activeLine.x, activeLine.Point.transform.position.x + activeLine.x), activeLine.Point.transform.position.y);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (Random.Range(0, 100) <= enemies[i].chanceToSpawn)
                        {
                            GameObject enemy = Instantiate(enemies[i].enemy, randomPlace.transform); ;
                            enemy.GetComponent<Actor>().level = DifficultyLevel.difLevel;

                            enemy.transform.parent = null;
                            break;
                        }
                    }
                    Destroy(randomPlace);

                    spawnCount--;
                }
                curSpawnRate = spawnRate;
            }
            else
            {
                ReloadSpawnRate();
            }
        }
    }

    public void ReloadSpawnRate()
    {
        if(activeLine.Point!=null)
        curSpawnRate -= Time.deltaTime;
    }


    public void OnDrawGizmosSelected()
    {
        foreach(Line line in lines)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(new Vector3(line.Point.transform.position.x - line.x, line.Point.transform.position.y), new Vector3(line.Point.transform.position.x + line.x, line.Point.transform.position.y));
            
        }
    }
}
[System.Serializable]
public class Line
{
    public GameObject Point;
    //public bool isLineActive = false;
    public float x;
}
[System.Serializable]
public class Enemy
{
    public GameObject enemy;
   [Range(0f,100f)] public float chanceToSpawn;
}


