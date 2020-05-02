using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public EventNames eventOnEndAnimation = EventNames.None;
    PoolableObject poolableObject;
    private void Awake()
    {
        poolableObject = GetComponent<PoolableObject>();
    }
    void OnEndAnimation()
    {
        poolableObject.ReturnToPool();
        EventManager.Invoke(eventOnEndAnimation);
    }
    public static GameObject InstantiateExplosion(GameObject explodingObj)
    {
        PoolableObject poolableObject = explodingObj.GetComponent<PoolableObject>();
        PoolsEnum explosionType = (poolableObject == null) ? PoolsEnum.EnemyExplosion :
            ObjParams.explosionType[poolableObject.objType];
        GameObject explosion = Pools.Get(explosionType);
        explosion.transform.position = explodingObj.transform.position;
        return explosion;
    }
}
