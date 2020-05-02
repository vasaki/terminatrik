using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelParameters
{
    int levelNumber = 0;
    Queue<int> enemies = new Queue<int>();
    int[] enemiesToSpawn = null;
    public bool LevelCompleted { get => enemies.Count == 0; }
    public int LevelNumber { get => levelNumber; }
    public int CountEnemies { get => enemies.Count; }
    public static int[] MaxScoreByLevel(int levels)
    {
        int[] maxScoreByLevel = new int[levels];
        int[] generatedEnemies = null;
        for (int i = 0; i<levels; i++)
        {
            generatedEnemies = GenerateNextLevelEnemiesQuantity(i, generatedEnemies);
            maxScoreByLevel[i] = 0;
            for (int j = 0; j < generatedEnemies.Length; j++)
                maxScoreByLevel[i] += GameConstants.EnemyScore[j] * generatedEnemies[j];
        }

        return maxScoreByLevel;
    }
    static int[] GenerateNextLevelEnemiesQuantity (int levelNumber = 0, 
        int[] currentLevelEnemiesQuantity = null)
    {
        int[] enemiesToSpawn =
            new int[GameConstants.FirstLevelEnemiesQuantity[MenuManager.Difficulty].Length];
        if (levelNumber == 0 || currentLevelEnemiesQuantity == null)
            Array.Copy(GameConstants.FirstLevelEnemiesQuantity[MenuManager.Difficulty],
                enemiesToSpawn, enemiesToSpawn.Length);
        else
        {
            Array.Copy(currentLevelEnemiesQuantity, enemiesToSpawn, enemiesToSpawn.Length);
            for (int i = 0; i < enemiesToSpawn.Length && i <= levelNumber; i++) 
                enemiesToSpawn[i] +=
                    GameConstants.AdditionalEnemiesQuantityPerLevel[MenuManager.Difficulty];
        }
        return enemiesToSpawn;
    }
    public void NextLevel()
    {
        float totalEnemies = 0f;
        int[] enemiesLeft = 
            new int[GameConstants.FirstLevelEnemiesQuantity[MenuManager.Difficulty].Length];
        levelNumber++;
        enemies.Clear();
        enemiesToSpawn = GenerateNextLevelEnemiesQuantity(levelNumber-1, enemiesToSpawn);
        Array.Copy(enemiesToSpawn, enemiesLeft, enemiesToSpawn.Length);
        for (int i = 0; i < enemiesToSpawn.Length; i++) totalEnemies += enemiesToSpawn[i];
        while (totalEnemies > 0)
        {
            float prob = UnityEngine.Random.Range(0f, 1f);
            float threshold = 0;
            int i = -1;
            while (prob > threshold && i < enemiesLeft.Length)
            {
                i++;
                threshold += enemiesLeft[i] / totalEnemies;
            }
            enemies.Enqueue(i);
            enemiesLeft[i]--;
            totalEnemies--;
        }
    }
    public int NextEnemy()
    {
        return enemies.Dequeue();
    }
}
