using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    Timer enemySpawningTimer;
    Timer pauseTimer;
    float[] enemiesSpawnTimeInterval;
    Sprite[] backgroundSprites;
    SpriteRenderer bgSpriteRenderer;
    LevelParameters levelParameters = new LevelParameters();
    int visibleHostileObject = 0;
    int totalScore = 0;
    static int currentScore = 0;
    StopWatch timeAliveTimer;
    float timeAlive = 0;
    Equipment equipment;

    public static float Gold => currentScore;

    private void Awake()
    {
        ScreenUtils.Initialize();
        Pools.Initialize();
        ObjParams.Initialize();
        backgroundSprites = Resources.LoadAll<Sprite>("Backgrounds");
        bgSpriteRenderer = 
            GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
        EventManager.AddListener(EventNames.HostileObjectAppeared, OnHostileObjectAppeared);
        EventManager.AddListener(EventNames.HostileObjectDisappeared,
            OnHostileObjectDisappeared);
        EventManager.AddListener(EventNames.GameTitleDisappeared, OnGameTitleDisappeared);
        EventManager.AddListener(EventNames.AddScore, OnScoreAdd);
        EventManager.AddListener(EventNames.PlayerDies, OnPlayerDies);
        int[] maxScoreByLevel = LevelParameters.MaxScoreByLevel(4);
        for (int i = 0; i < maxScoreByLevel.Length; i++)
            maxScoreByLevel[i] = 
                (int)(maxScoreByLevel[i] * GameConstants.PriceFactor[MenuManager.Difficulty]);
        equipment = new Equipment();
        equipment.Initialize();
        equipment.Add(EquipmentType.Laser, 0, EquipmentStatus.Owned);
        equipment.Add(EquipmentType.FanLaser, maxScoreByLevel[0], EquipmentStatus.NotAvailable);
        equipment.Add(EquipmentType.BackLaser, maxScoreByLevel[0], 
            EquipmentStatus.NotAvailable);
        equipment.Add(EquipmentType.TripleLaser, maxScoreByLevel[0] + maxScoreByLevel[1] +
            maxScoreByLevel[2], EquipmentStatus.NotAvailable);
        equipment.Add(EquipmentType.KillerShield, maxScoreByLevel[2], 
            EquipmentStatus.NotAvailable);
        equipment.Add(EquipmentType.StrongShield, maxScoreByLevel[3], 
            EquipmentStatus.NotAvailable);
        equipment.Add(EquipmentType.Bomb, maxScoreByLevel[2], EquipmentStatus.NotAvailable);
        equipment.Add(EquipmentType.TripleBomb, maxScoreByLevel[3], 
            EquipmentStatus.NotAvailable);
        equipment.Equip(EquipmentType.Laser);
        timeAliveTimer = gameObject.AddComponent<StopWatch>();
        timeAliveTimer.EventThreshold = 1;
        timeAliveTimer.AddListener(OnTimeAliveTimer);
    }
    public static bool SpendGold(float gold)
    {
        if (gold >= currentScore)
        {
            EventManager.Invoke(EventNames.NoGoldMessage);
            return false;
        }
        currentScore -= (int)gold;
        EventManager.Invoke(EventNames.GoldChange, new FloatParam(currentScore));
        return true;
    }
    private void OnTimeAliveTimer()
    {
        timeAlive = timeAliveTimer.ElapsedSeconds;
        EventManager.Invoke(EventNames.TimeAliveChanged, new FloatParam(timeAlive));
    }
    private void OnHostileObjectAppeared(EventParameter param)
    {
        visibleHostileObject++;
    }
    private void OnHostileObjectDisappeared(EventParameter param)
    {
        visibleHostileObject--;
        if (levelParameters.LevelCompleted && visibleHostileObject == 0) pauseTimer.Run();
    }
    private void Start()
    {
        //start enemy spawning timer
        enemySpawningTimer = gameObject.AddComponent<Timer>();
        enemySpawningTimer.AddListener(SpawnEnemy);
        //start pausing timer
        pauseTimer = gameObject.AddComponent<Timer>();
        pauseTimer.AddListener(OnPauseEnd);
        pauseTimer.Duration = GameConstants.DelayBetweenLevels;
        //pass equipment to player
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>()
            .Initialize(equipment);

        //EventManager.Invoke(EventNames.AddScore, new IntParam(100000));
    }
    private void Update()
    {
        //pause menu
        if (Input.GetKeyDown(KeyCode.Escape)) MenuManager.GoTo(MenuOptions.PauseMenu);
        else if (Input.GetKeyDown(KeyCode.LeftAlt))
            MenuManager.GoTo(MenuOptions.Shop, new EquipmentParam(equipment));
    }
    private void OnScoreAdd(EventParameter param)
    {
        currentScore += ((IntParam)param).AnInt;
        totalScore += ((IntParam)param).AnInt;
    }
    private void OnGameTitleDisappeared(EventParameter param)
    {
        //starts first level actually
        pauseTimer.Run();
    }
    private void OnPauseEnd()
    {
        StartNextLevel();
    }
    private void StartNextLevel()
    {
        if (!timeAliveTimer.Running) timeAliveTimer.StartWatch();
        levelParameters.NextLevel();
        EventManager.Invoke(EventNames.NewLevelStart,
            new IntParam(levelParameters.LevelNumber));
        EventManager.Invoke(EventNames.EnemiesQuantityChanged, 
            new IntParam(levelParameters.CountEnemies));
        enemiesSpawnTimeInterval = new float[2] { GameConstants.EnemiesSpawnTimeInterval[0],
            GameConstants.EnemiesSpawnTimeInterval[1] };
        if (levelParameters.LevelNumber > 1) bgSpriteRenderer.sprite = 
            backgroundSprites[UnityEngine.Random.Range(0, backgroundSprites.Length)];
        RunSpawnEnemyTimer();

        //Update Available Equipment
        if (levelParameters.LevelNumber < GameConstants.EquipmentBecomesAvailable.Length &&
            GameConstants.EquipmentBecomesAvailable[levelParameters.LevelNumber] != null)
            foreach (EquipmentType equipmentType in
                GameConstants.EquipmentBecomesAvailable[levelParameters.LevelNumber])
                equipment.ChangeStatus(equipmentType, EquipmentStatus.Available);
    }
    void SpawnEnemy()
    {
        Enemy newEnemy = null;
        switch (levelParameters.NextEnemy())
        {
            case 0:
                newEnemy = Pools.Get(PoolsEnum.Enemy1).GetComponent<Enemy>();
                break;
            case 1:
                newEnemy = Pools.Get(PoolsEnum.Enemy2).GetComponent<Enemy>();
                break;
            case 2:
                newEnemy = Pools.Get(PoolsEnum.Enemy3).GetComponent<Enemy>();
                break;
            case 3:
                newEnemy = Pools.Get(PoolsEnum.Enemy4).GetComponent<Enemy>();
                break;
            case 4:
                newEnemy = Pools.Get(PoolsEnum.Enemy5).GetComponent<Enemy>();
                break;
            case 5:
                newEnemy = Pools.Get(PoolsEnum.Enemy6).GetComponent<Enemy>();
                break;
        }
        newEnemy.AfterInstantiateFromPool();
        EventManager.Invoke(EventNames.EnemiesQuantityChanged,
            new IntParam(levelParameters.CountEnemies));
        if (!levelParameters.LevelCompleted) RunSpawnEnemyTimer();
    }
    void RunSpawnEnemyTimer()
    {
        enemySpawningTimer.Duration = 
            UnityEngine.Random.Range(enemiesSpawnTimeInterval[0],
            enemiesSpawnTimeInterval[1]);
        enemiesSpawnTimeInterval[0] *= GameConstants.SpeedUpFactor[MenuManager.Difficulty];
        enemiesSpawnTimeInterval[1] *= GameConstants.SpeedUpFactor[MenuManager.Difficulty];
        enemySpawningTimer.Run();
    }
    private void OnPlayerDies(EventParameter param)
    {
        MenuManager.GoTo(MenuOptions.PlayAgainMenu,
            new EndGameParam(totalScore, new TimeSpan(0, 0, (int)timeAlive)));
    }
    public static bool IsEnemyObject(GameObject obj)
    {
        return obj.CompareTag("Enemy") || obj.CompareTag("EnemyBullet");
    }

}
