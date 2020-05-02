using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class ObjectsPool <T>
{
    protected List<T> pool;
    public ObjectsPool(int capacity = 20)
    {
        pool = new List<T>(capacity);
    }

    protected void Initialize()
    {
        for (int i = 0; i < pool.Capacity; i++) pool.Add(GetNewObject());
    }

    public void ForEach(Action<T> action)
    {
        foreach (T obj in pool) action(obj);
    }

    public virtual T GetObject()
    {
        if (pool.Count > 0)
        {
            T obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            return obj;
        }
        else
        {
            pool.Capacity++;
            return GetNewObject();
        }
    }

    public virtual void ReturnObjectToPool(T obj)
    {
 
        pool.Add(obj);
    }

    protected abstract T GetNewObject();
}
