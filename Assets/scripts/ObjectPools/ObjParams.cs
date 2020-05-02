using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjParams
{
    private static float minimalEnemySpawnY;
    private static float maximalEnemySpawnY;
    public static Dictionary<PoolsEnum, PoolsEnum> enemyBulletType;
    public static Dictionary<PoolsEnum, PoolsEnum> explosionType;
    public static Dictionary<PoolsEnum, Vector3> bulletSpawnPositionDelta;
    public static Dictionary<PoolsEnum, float> enemySpawnX;
    public static Dictionary<PoolsEnum, float[]> enemyXRange;
    public static Dictionary<PoolsEnum, float[]> enemySpawnYRange;
    public static Dictionary<PoolsEnum, Vector2> enemyMovementDirection;
    public static Dictionary<PoolsEnum, float[]> enemyMovementSpeedRange;
    public static Dictionary<PoolsEnum, float> bulletSpawnTime;
    public static Dictionary<PoolsEnum, float> hp;
    public static Dictionary<PoolsEnum, float> bulletForce;
    public static Dictionary<PoolsEnum, int> score;
    
    private static void SpawnTestEnemyAndInitialize(PoolsEnum enemyType)
    {
        GameObject testEnemy = Pools.Get(enemyType);
        if (testEnemy.GetComponent<Enemy>().firingBullets)
        {
            //bullet spawning location
            Vector3 mouthPosition = testEnemy.transform.Find("Mouth").position;
            GameObject testBullet = Pools.Get(enemyBulletType[enemyType]);
            float bulletSizeX = testBullet.GetComponent<BoxCollider2D>().size.x;
            bulletSpawnPositionDelta.Add(enemyType,
                new Vector3(mouthPosition.x - bulletSizeX * 0.5f,
                mouthPosition.y, mouthPosition.z) - testEnemy.transform.position);
            testBullet.GetComponent<PoolableObject>().ReturnToPool();
        }
        //enemy spawning location
        Vector2 enemySize = testEnemy.GetComponent<BoxCollider2D>().size * 0.5f;
        enemySpawnX.Add(enemyType, ScreenUtils.ScreenRight + enemySize.x);
        enemySpawnYRange.Add(enemyType, new float[2]);
        enemySpawnYRange[enemyType][0] = minimalEnemySpawnY + enemySize.y;
        enemySpawnYRange[enemyType][1] = maximalEnemySpawnY - enemySize.y;
        //borders for enemies that can't leave the screen
        enemyXRange.Add(enemyType, new float[2]);
        enemyXRange[enemyType][0] = ScreenUtils.ScreenLeft + enemySize.x;
        enemyXRange[enemyType][1] = ScreenUtils.ScreenRight - enemySize.x;
        //return the object
        testEnemy.GetComponent<PoolableObject>().ReturnToPool();
    }
    private static void InitializeAnEnemy(PoolsEnum enemyType, PoolsEnum bulletType, 
        Vector2 enemyMovementDirection, float[] enemyMovementSpeedRange,
        float bulletSpawnTime, float hp, float bulletForce, int score)
    {
        enemyBulletType.Add(enemyType, bulletType);
        ObjParams.enemyMovementDirection.Add(enemyType, enemyMovementDirection);
        ObjParams.enemyMovementSpeedRange.Add(enemyType, enemyMovementSpeedRange);
        ObjParams.bulletSpawnTime.Add(enemyType, bulletSpawnTime);
        ObjParams.hp.Add(enemyType, hp);
        ObjParams.bulletForce.Add(enemyType, bulletForce);
        explosionType.Add(enemyType, PoolsEnum.EnemyExplosion);
        ObjParams.score.Add(enemyType, score);
        SpawnTestEnemyAndInitialize(enemyType);
    }
    private static void InitializeABullet(PoolsEnum bulletType, float hp, int score)
    {
        ObjParams.hp.Add(bulletType, hp);
        explosionType.Add(bulletType, PoolsEnum.BulletExplosion);
        ObjParams.score.Add(bulletType, score);
    }

    public static void Initialize()
    {
        enemyBulletType = new Dictionary<PoolsEnum, PoolsEnum>();
        explosionType = new Dictionary<PoolsEnum, PoolsEnum>();
        bulletSpawnPositionDelta = new Dictionary<PoolsEnum, Vector3>();
        enemySpawnX = new Dictionary<PoolsEnum, float>();
        enemyXRange = new Dictionary<PoolsEnum, float[]>();
        enemySpawnYRange = new Dictionary<PoolsEnum, float[]>();
        enemyMovementDirection = new Dictionary<PoolsEnum, Vector2>();
        enemyMovementSpeedRange = new Dictionary<PoolsEnum, float[]>();
        bulletSpawnTime = new Dictionary<PoolsEnum, float>();
        hp = new Dictionary<PoolsEnum, float>();
        bulletForce = new Dictionary<PoolsEnum, float>();
        score = new Dictionary<PoolsEnum, int>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 playerSize = player.GetComponent<BoxCollider2D>().size;
        Vector2 barrelPosition = player.transform.Find("Barrel").position;
        minimalEnemySpawnY = ScreenUtils.ScreenBottom +
            Mathf.Abs(player.transform.position.y - playerSize.y * 0.5f - barrelPosition.y);
        maximalEnemySpawnY = ScreenUtils.ScreenTop -
            Mathf.Abs(player.transform.position.y + playerSize.y * 0.5f - barrelPosition.y);

        InitializeAnEnemy(PoolsEnum.Enemy1, PoolsEnum.Enemy1Bullet,
            GameConstants.EnemyMovementDirection, GameConstants.EnemyMovementSpeedRange[0],
            GameConstants.EnemyBulletSpawnTime[0], GameConstants.EnemyHP[0],
            GameConstants.EnemyBulletForce[0], GameConstants.EnemyScore[0]);
        InitializeAnEnemy(PoolsEnum.Enemy2, PoolsEnum.Enemy2Bullet,
            GameConstants.EnemyMovementDirection, GameConstants.EnemyMovementSpeedRange[1],
            GameConstants.EnemyBulletSpawnTime[1], GameConstants.EnemyHP[1],
            GameConstants.EnemyBulletForce[1], GameConstants.EnemyScore[1]);
        InitializeAnEnemy(PoolsEnum.Enemy3, PoolsEnum.Enemy3Bullet,
            GameConstants.EnemyMovementDirection, GameConstants.EnemyMovementSpeedRange[2],
            GameConstants.EnemyBulletSpawnTime[2], GameConstants.EnemyHP[2],
            GameConstants.EnemyBulletForce[2], GameConstants.EnemyScore[2]);
        InitializeAnEnemy(PoolsEnum.Enemy4, PoolsEnum.Enemy4Bullet,
            GameConstants.EnemyMovementDirection, GameConstants.EnemyMovementSpeedRange[3],
            GameConstants.EnemyBulletSpawnTime[3], GameConstants.EnemyHP[3],
            GameConstants.EnemyBulletForce[3], GameConstants.EnemyScore[3]);
        InitializeAnEnemy(PoolsEnum.Enemy5, PoolsEnum.Enemy2,
            GameConstants.EnemyMovementDirection, GameConstants.EnemyMovementSpeedRange[4],
            GameConstants.EnemyBulletSpawnTime[4], GameConstants.EnemyHP[4],
            GameConstants.EnemyBulletForce[4], GameConstants.EnemyScore[4]);
        InitializeAnEnemy(PoolsEnum.Enemy6, PoolsEnum.Enemy4,
            GameConstants.EnemyMovementDirection, GameConstants.EnemyMovementSpeedRange[5],
            GameConstants.EnemyBulletSpawnTime[5], GameConstants.EnemyHP[5],
            GameConstants.EnemyBulletForce[5], GameConstants.EnemyScore[5]);
        InitializeABullet(PoolsEnum.Enemy1Bullet, GameConstants.EnemyBulletHP[0],
            GameConstants.EnemyBulletScore[0]);
        InitializeABullet(PoolsEnum.Enemy2Bullet, GameConstants.EnemyBulletHP[1],
            GameConstants.EnemyBulletScore[1]);
        InitializeABullet(PoolsEnum.Enemy3Bullet, GameConstants.EnemyBulletHP[2],
            GameConstants.EnemyBulletScore[2]);
    }


}
