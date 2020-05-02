using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBehaviour : MonoBehaviour
{
    float hp;
    float opacityDecrease;
    int score = 0;
    SpriteRenderer spriteRenderer;
    PoolableObject poolableObject;
    public float HP => hp;
    private void Awake()
    {
        poolableObject = GetComponent<PoolableObject>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize()
    {
        score = ObjParams.score[poolableObject.objType];
        Initialize(ObjParams.hp[poolableObject.objType]);
    }

    public void Initialize(float hp)
    {
        this.hp = hp;
        opacityDecrease = 1f / hp;
        SetOpacity(1);
    }

    private void SetOpacity(float opacity)
    {
        Color color = spriteRenderer.color;
        color.a = opacity;
        spriteRenderer.color = color;
    }
    public void GetDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            if (poolableObject != null) // HP behaviour can be used for shields and player
            {
                Explosion.InstantiateExplosion(gameObject);
                EventManager.Invoke(EventNames.AddScore, new IntParam { AnInt = score });
                poolableObject.ReturnToPool();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else SetOpacity(spriteRenderer.color.a - opacityDecrease * damage);
    }
}
