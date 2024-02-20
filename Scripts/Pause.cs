using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject mainPanel;
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider effectSlider;
    //public Toggle musicToggle;
    // Start is called before the first frame update
    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(MusicVolume);
        effectSlider.onValueChanged.AddListener(EffectsVolume);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("EffectsVolume", effectSlider.value);
    }
    void Start()
    {

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        effectSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        //MusicVolume(musicLoad);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartPause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ClosePause()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void Options()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    //public void ToggleMusic(bool enabled)
    //{
    //    if(!enabled)
    //    {
    //        musicSlider.value = musicSlider.minValue;
    //    }
    //    else
    //    {
    //        musicSlider.value = musicSlider.maxValue;
    //    }
    //}
    public void MusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value)*30f);

    }
    public void EffectsVolume(float value)
    {
        mixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 30f);

    }
    public void BackFromOptions()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        SaveStats.isSaved = false;
        DifficultyLevel.RefreshDifficulty();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void ExitToDesktop()
    {
        Application.Quit();
    }
    

}
