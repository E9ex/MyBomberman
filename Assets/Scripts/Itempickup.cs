using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itempickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }

    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;

            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionradius++;
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<MovementController>().speed++;
                break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            OnItemPickup(other.gameObject);
        }
    }
}
