using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructiontime = 1f;

    private void Start()
    {
        Destroy(gameObject,destructiontime);
    }
}
