using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GamePoolManager : MonoSingle<GamePoolManager>
{
    [System.Serializable]
    private class PoolData
    {
        public string poolName;
        public GameObject Item;
        public int ItemCounts;
    }
    [SerializeField]private List<PoolData> poolItems = new List<PoolData>();
    private Dictionary<string,Queue<GameObject>> poolDic = new Dictionary<string, Queue<GameObject>>();
    private GameObject parrent;

    private void Awake()
    {
        parrent = new GameObject("对象池父对象");
        parrent.transform.SetParent(this.transform);
        Init();
        Debug.Log("Awake字典中的元素数量: " + poolDic.Count);
        Debug.Log("Awake里PoolItems数量" + poolItems.Count);
    }
    private void Init()
    {
        for (int i = 0; i < poolItems.Count; i++)
        {
            for(int j = 0; j < poolItems[i].ItemCounts; j++)
            {
                GameObject item = Instantiate(poolItems[i].Item);
                item.SetActive(false);
                item.transform.SetParent(parrent.transform);
                //如果没有我们要的poolName
                if (!poolDic.ContainsKey(poolItems[i].poolName))
                {
                    poolDic.Add(poolItems[i].poolName, new Queue<GameObject>());
                }
                poolDic[poolItems[i].poolName].Enqueue(item);
            }

        }
    }

    public void TryGetPoolItem(string name)
    {
        if (!poolDic.ContainsKey(name))
        {
            Debug.Log("根本不存在该对象");
            Debug.Log("字典中的元素数量: " + poolDic.Count);
            Debug.Log("PoolItems数量" + poolItems.Count);
            return;
        }
        GameObject temp = poolDic[name].Dequeue();
        temp.SetActive(true);
        poolDic[name].Enqueue(temp);


    }
}
