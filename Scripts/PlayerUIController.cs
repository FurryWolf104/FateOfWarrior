using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("Objects")]
    public Actor obj;
    public Player playerClass;
    public GameObject curHPBar;
    public GameObject maxHpBar;
    public GameObject levelBar;
    public DifficultyLevel difficultyLevel;

    [Header("TimerPanel")]
    public Text difValue;
    public Text playedTime;
    public Text TimeToChangeLvl;
    //int timer;

    [Header("PortalTimer")]
    public GameObject portalTimePanel;
    public Text CurPortalTime;
    public Text PortalTime;

    //public GameObject curExpBar;
    //public GameObject expToLvlBar;

    [Header("Sliders")]
    public Slider healthBarSlider;
    public Slider expBarSlider;

    [Header("Inventory")]
    public Text ironKeys;
    public Text goldenKeys;

    [Header("Stats")]
    public Text DamageValue;
    public Text ArmorValue;
    public Text SpeedValue;
    public Text RegenerationValue;
    void Start()
    {
        UpdateValues();
        UpdateSliders();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        UpdateSliders();
    }
    void UpdateValues()
    {
        curHPBar.GetComponent<Text>().text = obj.curHealth.ToString("0");
        maxHpBar.GetComponent<Text>().text = obj.maxHealth.ToString("0");
        levelBar.GetComponent<Text>().text = obj.level.ToString("0");

        ironKeys.text = playerClass.ironKeys.ToString();
        goldenKeys.text = playerClass.goldKeys.ToString();

        DamageValue.text = obj.damage.ToString();
        ArmorValue.text = (obj.armor*100).ToString()+"%";
        SpeedValue.text = playerClass.Speed.ToString();
        RegenerationValue.text = playerClass.regenerationSpeed.ToString("0.0");

        difValue.text = DifficultyLevel.difLevel.ToString();
        //timer = int.Parse(difficultyLevel.timer.ToString());
        //playedTime.text = (timer/60).ToString("0")+":"+(timer%60).ToString("0");
        playedTime.text = (DifficultyLevel.timer/60).ToString("0")+":"+(DifficultyLevel.timer%60).ToString("0");
        TimeToChangeLvl.text = (DifficultyLevel.ChangeLevelTimer / 60).ToString("0") + ":" + (DifficultyLevel.ChangeLevelTimer % 60).ToString("0");
        //curExpBar.GetComponent<Text>().text = obj.curExp.ToString("0");
        //expToLvlBar.GetComponent<Text>().text = obj.expToLvl.ToString("0");
        
        CurPortalTime.text = TeleportAltar.curPortalTime.ToString();
        PortalTime.text = TeleportAltar.portalTime.ToString();
    }
    public void UpdateSliders()
    {
        healthBarSlider.maxValue = obj.maxHealth;
        healthBarSlider.value = obj.curHealth;
        expBarSlider.maxValue = obj.expToLvl;
        expBarSlider.value = obj.curExp;
    }
    public void PortalPanelState(bool state)
    {
        portalTimePanel.SetActive(state);
    }
    
}
