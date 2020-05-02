using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    HPBehaviour hp;
    private void Awake()
    {
        hp = gameObject.GetComponent<HPBehaviour>();
    }

    public void Initialize(float hp)
    {
        this.hp.Initialize(hp);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.IsEnemyObject(collision.gameObject))
        {
            HPBehaviour enemyHP = collision.gameObject.GetComponent<HPBehaviour>();
            float hit = Mathf.Min(hp.HP, enemyHP.HP);
            enemyHP.GetDamage(hit);
            hp.GetDamage(hit);
        }
    }
}
