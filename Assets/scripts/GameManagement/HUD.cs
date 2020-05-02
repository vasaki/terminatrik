using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    Timer helpTextTimer;
    int enemies = 0;
    int currentScore = 0;
    Text ScoreText;
    Text PrimaryEquipmentText;
    Text SecondaryEquipmentText;
    float timeAlive = 0;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText = transform.Find("ScoreText").gameObject.GetComponent<Text>();
        SecondaryEquipmentText = transform.Find("SecondaryEquipmentText").gameObject.
            GetComponent<Text>();
        PrimaryEquipmentText = transform.Find("PrimaryEquipmentText").gameObject.
            GetComponent<Text>();

        EventManager.AddListener(EventNames.NewLevelStart, OnLevelStart);
        EventManager.AddListener(EventNames.EnemiesQuantityChanged, OnEnemiesQuantityChanged);
        EventManager.AddListener(EventNames.AddScore, OnScoreAdd);
        EventManager.AddListener(EventNames.TimeAliveChanged, OnTimeAliveChanged);
        EventManager.AddListener(EventNames.GoldChange, OnGoldChange);
        EventManager.AddListener(EventNames.NoGoldMessage, OnNoGoldMessage);
        EventManager.AddListener(EventNames.EquipmentStatusChanged, OnEquipmentStatusChanged);

        ChangeScoreText();
        helpTextTimer = gameObject.AddComponent<Timer>();
        helpTextTimer.Duration = GameConstants.HelpTextDelay;
        helpTextTimer.AddListener(OnHelpTextTimer);
        helpTextTimer.Run();

        //display game title
        Instantiate(Resources.Load("Menus/TitleText"), transform);
    }
    private void OnEquipmentStatusChanged(EventParameter param)
    {
        PrimaryEquipmentText.text = GameConstants.PrimaryText +
            GameConstants.EquipmentNames[Equipment.Primary];
        SecondaryEquipmentText.text = GameConstants.SecondaryText +
            GameConstants.EquipmentNames[Equipment.Secondary];
        if (Equipment.Secondary != EquipmentType.None)
            SecondaryEquipmentText.text += GameConstants.CostText +
                Equipment.Price(Equipment.Secondary);
    }
    private void OnGoldChange(EventParameter param)
    {
        currentScore = (int)((FloatParam)param).Float;
        ChangeScoreText();
    }
    private void OnScoreAdd(EventParameter intParam)
    {
        currentScore += ((IntParam)intParam).AnInt;
        ChangeScoreText();
    }
    private void OnTimeAliveChanged(EventParameter floatParameter)
    {
        timeAlive = ((FloatParam)floatParameter).Float;
        ChangeScoreText();
    }
    private void OnHelpTextTimer()
    {
        Instantiate(Resources.Load("Menus/HelpText"), transform);
    }
    private void ChangeScoreText()
    {
        ScoreText.text = GameConstants.ScoreTextTitle + currentScore +
            GameConstants.EnemiesTextTitle + enemies +
            GameConstants.TimeTextTitle + new TimeSpan(0,0,(int)timeAlive).ToString(@"mm\:ss");
    }
    private void OnEnemiesQuantityChanged(EventParameter param)
    {
        enemies = ((IntParam)param).AnInt;
        ChangeScoreText();
    }
    public void OnScoreChange(EventParameter intParam)
    {
        currentScore += ((IntParam)intParam).AnInt;
        ChangeScoreText();
    }
    private void OnLevelStart(EventParameter intParam)
    {
        GameObject levelText = (GameObject)Instantiate(Resources.Load("Menus/LevelText"), 
            transform);
        levelText.GetComponent<Text>().text =
            GameConstants.LevelText + ((IntParam)intParam).AnInt;
    }
    private void OnNoGoldMessage(EventParameter param)
    {
        Instantiate(Resources.Load("Menus/NoGoldText"), transform);
    }
}
