using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    public static float RobotMovementUnitsPerSecond = 0.045f;
    public static Vector2 PlayerBulletDirection = new Vector2(1, 0);
    public static float PlayerBulletAngleDegrees = 0;
    public static float FanBulletAngleDegrees = 30;
    public static float BackBulletAngleDegrees = 170;
    public static float TripleLaserPositionDelta = 0.1f;
    public static float FanLaserPositionDelta = 0.2f;
    public static int ShieldHP = 20;
    public static int StrongShieldHP = 50;
    public static string ShieldPrefab = "Equipment/Shield";
    public static string StrongShieldPrefab = "Equipment/StrongShield";
    public static string BombPrefab = "Equipment/Bomb";
    public static string TripleBombPrefab = "Equipment/TripleBomb";
    public static float LaserDistanceFromBomb = 0.1f;
    public static int BombLaserAngleStep = 10;
    public static int BombTriggersOnQuantity = 5;
    public static int TripleBombTriggersOnQuantity = 8;
    public static int TripleBombLaserIntensity = 3;
    public static int BombLaserIntensity = 1;
    public static Vector2 BackLaserPositionDelta = new Vector2(-0.7f, 0.2f);
    public static Vector2 EnemyMovementDirection = new Vector2(-1, 0);
    public static float PlayerBulletForce = 5f;
    public static float PlayerBombForce = 1f;
    public static int PoolSizePlayerBullet = 10;
    public static int[] PoolSizeEnemy = { 5, 5, 5, 5, 5, 5 };
    public static int[] PoolSizeEnemyBullet = { 5, 5, 5, 0, 0, 0 };
    public static float[][] EnemyMovementSpeedRange = { new float[2] { 0.05f, 0.3f },
        new float[2] { 0.3f, 0.7f }, new float[2] { 0.7f, 1.1f}, new float[2] {1.1f, 1.5f },
        new float[2] { 0.05f, 0.2f }, new float[2] { 0.05f, 0.2f }};
    public static float[] NextEnemySpawnedTimeRange = { 3f, 7f };
    public static float[] EnemyBulletForce = { 1f, 1.5f, 2f, 0f, 0f, 0f };
    public static float[] EnemyBulletSpawnTime = { 2.5f, 2f, 1.5f, 0f, 2.5f, 1f };
    public static List<PoolsEnum> EnemiesList = new List<PoolsEnum>( 
        new PoolsEnum[] {PoolsEnum.Enemy1, PoolsEnum.Enemy2, PoolsEnum.Enemy3,
            PoolsEnum.Enemy4, PoolsEnum.Enemy5, PoolsEnum.Enemy6});
    public static List<PoolsEnum> EnemyBulletsList = new List<PoolsEnum>(
        new PoolsEnum[] { PoolsEnum.Enemy1Bullet, PoolsEnum.Enemy2Bullet,
        PoolsEnum.Enemy3Bullet, PoolsEnum.Enemy4Bullet, PoolsEnum.Enemy5Bullet,
        PoolsEnum.Enemy6Bullet});
    public static float[] EnemyBulletHP = { 1f, 2f, 3f, 0f, 0f, 0f };
    public static float[] EnemyHP = { 2f, 4f, 6f, 7f, 30f, 40f };
    public static float[] EnemiesSpawnTimeInterval = { 3f, 7f };
    public static int[] EnemyScore = { 10, 20, 40, 60, 200, 300 };
    public static int[] EnemyBulletScore = { 1, 4, 6, 0, 0, 0 };
    public static string ScoreTextTitle = "GOLD: ";
    public static string EnemiesTextTitle = " ENEMIES: ";
    public static string TimeTextTitle = " TIME: ";
    public static string CostText = " COST: ";
    public static string PrimaryText = "PRIMARY: ";
    public static string SecondaryText = "SECONDARY: ";
    public static Color SecondaryEquipmentColor = Color.green;
    public static float[] ChangeDirectionTime = { 2f, 4f };
    public static float[] ChangeDirectionAngleDeg = { -30f, 30f };
    public static Dictionary<GameDifficulty, float> PriceFactor =
        new Dictionary<GameDifficulty, float>
        {
            {GameDifficulty.Easy, 0.8f },
            {GameDifficulty.Normal, 0.95f },
            {GameDifficulty.Hard, 1.05f }
        };
    public static Dictionary<GameDifficulty, float> SpeedUpFactor = 
        new Dictionary<GameDifficulty, float>
        {
            {GameDifficulty.Easy, 0.99f },
            {GameDifficulty.Normal, 0.95f },
            {GameDifficulty.Hard, 0.90f }
        };
    public static Dictionary<GameDifficulty, int[]> FirstLevelEnemiesQuantity =
        new Dictionary<GameDifficulty, int[]>
        {
            {GameDifficulty.Easy, new int[6] {5, 0, 0, 0, 0, 0} },
            {GameDifficulty.Normal, new int[6] {8, 0, 0, 0, 0, 0} },
            {GameDifficulty.Hard, new int[6] {13, 3, 0, 0, 0, 0} }
        };
    public static Dictionary<GameDifficulty, int> AdditionalEnemiesQuantityPerLevel =
        new Dictionary<GameDifficulty, int>
        {
            {GameDifficulty.Easy, 5 },
            {GameDifficulty.Normal, 8 },
            {GameDifficulty.Hard, 13 }
        };
    public static string LevelText = "LEVEL\n";
    public static float DelayBetweenLevels = 4f;
    public static float HelpTextDelay = 14f;
    public static Dictionary<GameDifficulty, string> GameDifficultyString =
        new Dictionary<GameDifficulty, string>
        {
            {GameDifficulty.Easy, "EASY" },
            {GameDifficulty.Normal, "NORMAL" },
            {GameDifficulty.Hard, "HARD" }
        };
    public static string GameDifficultyModeTitle = " DIFFICULTY";
    public static Dictionary<EquipmentType, string> EquipmentNames =
        new Dictionary<EquipmentType, string>
        {
            {EquipmentType.TripleLaser, "Triple Laser" },
            {EquipmentType.Laser, "Laser" },
            {EquipmentType.FanLaser, "Fan Laser" },
            {EquipmentType.BackLaser, "Backward Laser" },
            {EquipmentType.KillerShield, "Killer Shield" },
            {EquipmentType.StrongShield, "Monster Shield" },
            {EquipmentType.Bomb, "Simple Bomb" },
            {EquipmentType.TripleBomb, "Triple Bomb" },
            {EquipmentType.None, "None" }
        };
    public static Dictionary<EquipmentType, EquipmentHand> EquipmentHands =
        new Dictionary<EquipmentType, EquipmentHand>
        {
            {EquipmentType.TripleLaser, EquipmentHand.Primary },
            {EquipmentType.Laser, EquipmentHand.Primary },
            {EquipmentType.FanLaser, EquipmentHand.Primary },
            {EquipmentType.BackLaser, EquipmentHand.Primary },
            {EquipmentType.KillerShield, EquipmentHand.Secondary },
            {EquipmentType.StrongShield, EquipmentHand.Secondary },
            {EquipmentType.Bomb, EquipmentHand.Secondary },
            {EquipmentType.TripleBomb, EquipmentHand.Secondary }
        };
    public static Dictionary<EquipmentType, string> EquipmentDesciptions =
        new Dictionary<EquipmentType, string>
        {
            {EquipmentType.TripleLaser, "Primary. Three laser beams with one single shot. " +
                "You don't need to aim carefully any longer!"},
            {EquipmentType.Laser, "Primary. Most simple shooting weapon. You have it from " +
                "very first stage"},
            {EquipmentType.FanLaser, "Primary. Three laser beams shooting fan style. Maybe " +
                "you can hit something you didn't expect!"},
            {EquipmentType.BackLaser, "Primary. You can hit stuff even behind your back!"},
            {EquipmentType.KillerShield, "Secondary. A circle shield that protects you by "+
                "killing anything that hits it."},
            {EquipmentType.StrongShield, "Secondary. Much better shield!" },
            {EquipmentType.Bomb, "Secondary. Simple bomb that explodes in bunch of laser "+
                "beams when senses few enemy objects in close distance"},
            {EquipmentType.TripleBomb, "Secondary. Three bombs explode thrice as stronger"}
        };
    public static Dictionary<EquipmentType, string> EquipmentPrefab =
        new Dictionary<EquipmentType, string>
        {
            {EquipmentType.TripleLaser, "Equipment/TripleLaserIcon"},
            {EquipmentType.Laser, "Equipment/LaserIcon"},
            {EquipmentType.FanLaser, "Equipment/FanLaserIcon"},
            {EquipmentType.BackLaser, "Equipment/BackLaserIcon"},
            {EquipmentType.KillerShield, "Equipment/ShieldIcon"},
            {EquipmentType.StrongShield, "Equipment/StrongShieldIcon"},
            {EquipmentType.Bomb, "Equipment/BombIcon"},
            {EquipmentType.TripleBomb, "Equipment/TripleBombIcon"}
        };
    public static List<EquipmentType>[] EquipmentBecomesAvailable =
        new List<EquipmentType>[7]
        {
            null, null,
            new List<EquipmentType> {EquipmentType.FanLaser, EquipmentType.BackLaser },
            null, 
            new List<EquipmentType> {EquipmentType.TripleLaser},
            new List<EquipmentType> {EquipmentType.KillerShield, EquipmentType.StrongShield},
            new List<EquipmentType> {EquipmentType.Bomb, EquipmentType.TripleBomb} 
        };
}
