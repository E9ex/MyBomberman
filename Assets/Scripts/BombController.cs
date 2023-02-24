using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    
    [Header("BombCC")]
    public GameObject bombprefab;
    public KeyCode inputkey = KeyCode.Space;
    public float bombfusetime = 3f;
    public int bombAmount = 1;
    private int bombsRemaining;//1.49

    [Header("Explosion")]
    public Explosion Explosionprefab;

    public LayerMask ExploisonLayerMask;
    public float explosionduration = 1f;
    public int explosionradius = 1;

    [Header("Destructible")] 
    public Tilemap Destructibletiles;

    public Destructible DestructiblePrefab;
    

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    private void Update()
    {
        if (bombsRemaining>0&&Input.GetKeyDown(inputkey))
        {
            StartCoroutine(placebomb());
        }
    }

    private IEnumerator placebomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);//mathf because maybe place bomb between tiles then it may not align perfectly yes ıam erherher.
        position.y = Mathf.Round(position.y);
        GameObject bomb = Instantiate(bombprefab, position, quaternion.identity);
        bombsRemaining--;
        yield return new WaitForSeconds(bombfusetime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(Explosionprefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionduration);
        Destroy(explosion.gameObject,explosionduration);
         explode(position,Vector2.up,explosionradius);
         explode(position,Vector2.down,explosionradius);
         explode(position,Vector2.left,explosionradius);
         explode(position,Vector2.right,explosionradius);

        
        Destroy(bomb);
        bombsRemaining++;



    }

    private void explode(Vector2 position, Vector2 direction,int length)
    {
        if (length<=0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position,Vector2.one/2f,ExploisonLayerMask))//returns collider
        {
            ClearDestructible(position);
            return;
        }
        Explosion explosion = Instantiate(Explosionprefab, position, quaternion.identity);
        explosion.SetActiveRenderer(length>1 ? explosion.middle: explosion.end);//????wtf
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionduration);
        Destroy(explosion.gameObject,explosionduration);
        explode(position,direction,length-1);//length bitene kdr patlar.
        
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = Destructibletiles.WorldToCell(position);
        TileBase tile = Destructibletiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(DestructiblePrefab, position, Quaternion.identity);
            Destructibletiles.SetTile(cell,null);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;// bombanin içinden geçemiyoruz.
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombsRemaining++;
    }
}
