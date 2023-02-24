using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class Destructible : MonoBehaviour
{
    public float destructiontime = 1f;
    [Range(0f,1f)]
    public float itemSpawnChance = .2f;
    public GameObject[] spawnableItems;
    

    private void Start()
    {
        Destroy(gameObject,destructiontime);
    }

    
    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }




    

}
