using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject mainPanel;
    public GameObject howToPlayPanel;
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider effectSlider;

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(MusicVolume);
        effectSlider.onValueChanged.AddListener(EffectsVolume);
        if(!PlayerPrefs.HasKey("MusicVolume")) PlayerPrefs.SetFloat("MusicVolume", 1);
        if (!PlayerPrefs.HasKey("EffectsVolume")) PlayerPrefs.SetFloat("EffectsVolume", 1);
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
    public void MusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 30f);

    }
    public void EffectsVolume(float value)
    {
        mixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 30f);

    }
    public static void NewGame()
    {
        //Time.timeScale = 1;
        DifficultyLevel.RefreshDifficulty();
        SaveStats.isSaved = false;
        SceneManager.LoadScene(1);
    }
    public void Options()
    {
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void BackFromOptions()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void HowToPlay()
    {
        mainPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void BackFromHowToPlay()
    {
        mainPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
    }
    [ContextMenu("ClearPrefs")]
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    public static void ExitFromGame()
    {
        Application.Quit();
    }
}
