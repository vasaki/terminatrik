using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Pools
{
    public static Dictionary<PoolsEnum, GameObjectsPool> pools;

    static public void Initialize()
    {
        pools = new Dictionary<PoolsEnum, GameObjectsPool>();

        pools.Add(PoolsEnum.PlayerBullet, new GameObjectsPool("Bullets/PlayerBullet", 
            GameConstants.PoolSizePlayerBullet));
        pools.Add(PoolsEnum.Enemy1Bullet, new GameObjectsPool("Bullets/Enemy1Bullet",
            GameConstants.PoolSizeEnemyBullet[0]));
        pools.Add(PoolsEnum.Enemy2Bullet, new GameObjectsPool("Bullets/Enemy2Bullet",
            GameConstants.PoolSizeEnemyBullet[1]));
        pools.Add(PoolsEnum.Enemy3Bullet, new GameObjectsPool("Bullets/Enemy3Bullet",
            GameConstants.PoolSizeEnemyBullet[2]));
        pools.Add(PoolsEnum.Enemy1, new GameObjectsPool("Enemies/Enemy1",
            GameConstants.PoolSizeEnemy[0]));
        pools.Add(PoolsEnum.Enemy2, new GameObjectsPool("Enemies/Enemy2",
            GameConstants.PoolSizeEnemy[1]));
        pools.Add(PoolsEnum.Enemy3, new GameObjectsPool("Enemies/Enemy3",
            GameConstants.PoolSizeEnemy[2]));
        pools.Add(PoolsEnum.Enemy4, new GameObjectsPool("Enemies/Enemy4",
            GameConstants.PoolSizeEnemy[3]));
        pools.Add(PoolsEnum.Enemy5, new GameObjectsPool("Enemies/Enemy5",
            GameConstants.PoolSizeEnemy[4]));
        pools.Add(PoolsEnum.Enemy6, new GameObjectsPool("Enemies/Enemy6",
            GameConstants.PoolSizeEnemy[5]));
        pools.Add(PoolsEnum.BulletExplosion, new GameObjectsPool("Explosions/BulletExplosion",
            GameConstants.PoolSizePlayerBullet+GameConstants.PoolSizeEnemyBullet[0]+
            GameConstants.PoolSizeEnemyBullet[1]+GameConstants.PoolSizeEnemyBullet[2]));
        pools.Add(PoolsEnum.EnemyExplosion, new GameObjectsPool("Explosions/EnemyExplosion",
            GameConstants.PoolSizeEnemy[0]+GameConstants.PoolSizeEnemy[1]+
            GameConstants.PoolSizeEnemy[2]+GameConstants.PoolSizeEnemy[3]+
            GameConstants.PoolSizeEnemy[4]));
    }

    static public GameObject Get(PoolsEnum type)
    {
        return pools[type].GetObject();
    }

    static public void Return(PoolsEnum type, GameObject obj)
    {
        pools[type].ReturnObjectToPool(obj);
    }

}
