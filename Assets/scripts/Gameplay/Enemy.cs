using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Timer firingTimer;
    Timer directionTimer;
    public PoolsEnum objType;
    RBBehaviour rbb;
    GameObject player;
    bool initialized = false;
    [SerializeField]
    public bool firingBullets = true;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        objType = GetComponent<PoolableObject>().objType;
        rbb = GetComponent<RBBehaviour>();
    }
    private void SetDirectionAndRestartTimer(float angleDeg)
    {
        rbb.SetDirection(angleDeg);
        directionTimer.Restart();
    }
    private void Update()
    {
        if (objType == PoolsEnum.Enemy4 && player != null)
        {
            Vector2 newDirection = player.transform.position - transform.position;
            newDirection.Normalize();
            rbb.Velocity = newDirection * rbb.Velocity.magnitude;
        }
        else if (IsEnemyChangingDirection())
        {
            if (transform.position.y >= ObjParams.enemySpawnYRange[objType][1] &&
                rbb.Velocity.y >= 0)
                SetDirectionAndRestartTimer(Random.Range(180, 360));
            else if (transform.position.y <= ObjParams.enemySpawnYRange[objType][0] &&
                rbb.Velocity.y <= 0)
                SetDirectionAndRestartTimer(Random.Range(0, 180));
            if (transform.position.x >= ObjParams.enemyXRange[objType][1] && 
                rbb.Velocity.x >= 0)
                SetDirectionAndRestartTimer(Random.Range(90, 270));
            else if (transform.position.x <= ObjParams.enemyXRange[objType][0] &&
                rbb.Velocity.x <= 0)
                SetDirectionAndRestartTimer(Random.Range(270, 450));
        }
    }
    private bool IsEnemyChangingDirection()
    {
        return (objType == PoolsEnum.Enemy5 || objType == PoolsEnum.Enemy6);
    }
    public void Initialize()
    {
        initialized = true;
        if (firingBullets)
        {
            firingTimer = gameObject.AddComponent<Timer>();
            firingTimer.AddListener(Fire);
            firingTimer.Duration = ObjParams.bulletSpawnTime[objType];
        }
        if (IsEnemyChangingDirection())
        {
            directionTimer = gameObject.AddComponent<Timer>();
            directionTimer.AddListener(ChangeDirection);
            directionTimer.Duration = Random.Range(GameConstants.ChangeDirectionTime[0],
                GameConstants.ChangeDirectionTime[1]);
        }
    }
    private void ChangeDirection()
    {
        rbb.RotateDirection(Random.Range(GameConstants.ChangeDirectionAngleDeg[0],
            GameConstants.ChangeDirectionAngleDeg[1]));
        directionTimer.Run();
    }
    private void Fire()
    {
        if (objType == PoolsEnum.Enemy5)
        {
            Enemy newEnemy = Pools.Get(PoolsEnum.Enemy2).GetComponent<Enemy>();
            newEnemy.AfterInstantiateFromPool(transform.position + 
                ObjParams.bulletSpawnPositionDelta[objType]);
        }
        else if (objType == PoolsEnum.Enemy6)
        {
            Enemy newEnemy = Pools.Get(PoolsEnum.Enemy4).GetComponent<Enemy>();
            newEnemy.AfterInstantiateFromPool(transform.position +
                ObjParams.bulletSpawnPositionDelta[objType]);
        }
        else
        {
            GameObject bullet = Pools.Get(ObjParams.enemyBulletType[objType]);
            bullet.transform.position = gameObject.transform.position +
                ObjParams.bulletSpawnPositionDelta[objType];
            bullet.GetComponent<RBBehaviour>().ApplyForce(GameConstants.EnemyMovementDirection *
                ObjParams.bulletForce[objType], ForceMode2D.Impulse);
            bullet.GetComponent<HPBehaviour>().Initialize();
        }
        firingTimer.Run();
    }
    public void StartMoving()
    {
        if (!initialized) Initialize();
        rbb.ApplyForce(ObjParams.enemyMovementDirection[objType] *
            Random.Range(ObjParams.enemyMovementSpeedRange[objType][0], 
                ObjParams.enemyMovementSpeedRange[objType][1]),
            ForceMode2D.Impulse);
        //and Firing
        if (firingBullets) Fire();
        //and changing Direction;
        if (IsEnemyChangingDirection()) directionTimer.Run();
    }
    public void ChangeStartingPosition()
    {
        transform.position = new Vector2 (ObjParams.enemySpawnX[objType],
            Random.Range(ObjParams.enemySpawnYRange[objType][0],
                ObjParams.enemySpawnYRange[objType][1]));
    }
    public void AfterInstantiateFromPool(Vector2 pos = default)
    {
        if (pos == default) ChangeStartingPosition();
        else transform.position = pos;
        StartMoving();
        GetComponent<HPBehaviour>().Initialize();
    }
}
