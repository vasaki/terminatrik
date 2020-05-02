using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPool : ObjectsPool<GameObject>
{
    string prefab;
    public int Count => pool.Count;
    public int Capacity => pool.Count;
    public GameObjectsPool(string prefabResourceName, int capacity = 20) : base (capacity)
    {
        prefab = prefabResourceName;
        Initialize();
    }

    public override GameObject GetObject()
    {
        GameObject obj = base.GetObject();
        obj.SetActive(true);
        return obj;
    }

    public override void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        base.ReturnObjectToPool(obj);

    }

    protected override GameObject GetNewObject()
    {
        GameObject newObj = (GameObject)Object.Instantiate(Resources.Load(prefab));
        newObj.SetActive(false);
        return newObj;
    }
}
