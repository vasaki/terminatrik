using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    PoolableObject poolableObject;
    RBBehaviour rbb;
    private void Awake()
    {
        poolableObject = GetComponent<PoolableObject>();
        rbb = GetComponent<RBBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.IsEnemyObject(collision.gameObject))
        {
            GameObject explosion = Pools.Get(PoolsEnum.BulletExplosion);
            explosion.transform.position = gameObject.transform.position;
            poolableObject.ReturnToPool();
            collision.gameObject.GetComponent<HPBehaviour>().GetDamage(1);
        }
    }

    public void Fire(float angle)
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(0,0,1), angle);
        Vector3 direction = transform.rotation * GameConstants.PlayerBulletDirection;

        rbb.ApplyForce(direction * GameConstants.PlayerBulletForce, ForceMode2D.Impulse);
    }
}
