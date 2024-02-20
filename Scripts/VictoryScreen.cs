using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("BackToReality")]
    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        DifficultyLevel.RefreshDifficulty();
        SceneManager.LoadScene(0);
    }
    
    public void Retry()
    {
        Time.timeScale = 1;
        SaveStats.isSaved = false;
        DifficultyLevel.RefreshDifficulty();
        SceneManager.LoadScene(1);
    }
    
}
