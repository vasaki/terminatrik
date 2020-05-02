using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    [SerializeField]
    public PoolsEnum objType;
    protected RBBehaviour rbb;

    protected virtual void Awake()
    {
        rbb = GetComponent<RBBehaviour>();
    }
    private void OnBecameVisible()
    {
        if (GameConstants.EnemiesList.Contains(objType) ||
            GameConstants.EnemyBulletsList.Contains(objType))
                EventManager.Invoke(EventNames.HostileObjectAppeared);

    }
    private void OnBecameInvisible()
    {
        ReturnToPool();
        if (GameConstants.EnemiesList.Contains(objType) ||
            GameConstants.EnemyBulletsList.Contains(objType))
                EventManager.Invoke(EventNames.HostileObjectDisappeared);
    }
    public void ReturnToPool()
    {
        if (gameObject.activeSelf)
        {
            if (rbb != null) rbb.StopMovement();
            Pools.Return(objType, gameObject);
        }
    }

}
