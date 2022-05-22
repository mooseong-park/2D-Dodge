using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Items
    {
        public Transform item;
    }

    public Items[] items;
    public Transform[] spawnPoints;
    public float itemSpawnCountdown = 1f;
    public int spawnItemIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(itemSpawnCountdown <= 0)
        {
            spawnItemIndex = Random.Range(0, 1);
            SpawnItem(items[spawnItemIndex]);
            itemSpawnCountdown = Random.Range(3, 5);
        }

        else
        {
            itemSpawnCountdown -= Time.deltaTime;
        }
    }

    void SpawnItem (Items _items)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        ObjectPooler.SpawnFromPool<Item_A>("Item_A", _sp.position, _sp.rotation);
    }
}
