using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    int quantityToTriggerExplosion;
    int enemyObjectsQuantity = 0;
    int laserIntensity = 0;
    int angleStep;
    public void Initialize(int quantityToTriggerExplosion, int angleStep, int laserIntensity)
    {
        this.quantityToTriggerExplosion = quantityToTriggerExplosion;
        this.angleStep = angleStep;
        this.laserIntensity = laserIntensity;
    }
    private void SpawnBullet(Vector3 direction, int angle)
    {
        GameObject bullet = Pools.Get(PoolsEnum.PlayerBullet);
        bullet.transform.position = transform.position + direction;
        bullet.GetComponent<PlayerBullet>().Fire(angle);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.IsEnemyObject(collision.gameObject))
        {
            enemyObjectsQuantity++;
            if (enemyObjectsQuantity >= quantityToTriggerExplosion)
            {
                int angle = 0;
                float angleRad;
                float sin;
                float cos;
                Vector3 direction = new Vector3(1, 0, 0) * GameConstants.LaserDistanceFromBomb;
                while (angle < 360)
                {
                    for (int i = 0; i < laserIntensity; i++) SpawnBullet(direction, angle);
                    angle += angleStep;
                    angleRad = angle * Mathf.Deg2Rad;
                    sin = Mathf.Sin(angleRad);
                    cos = Mathf.Cos(angleRad);
                    direction.x = direction.x * cos - direction.y * sin;
                    direction.y = direction.x * sin + direction.y * cos;
                }
                Explosion.InstantiateExplosion(gameObject);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.IsEnemyObject(collision.gameObject)) enemyObjectsQuantity--;
    }
}
