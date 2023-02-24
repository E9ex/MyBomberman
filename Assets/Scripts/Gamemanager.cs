using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public GameObject[] players;

    public void CheckWinstate()
    {
        int aliveCount = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                aliveCount++;
            }
        }

        if (aliveCount<=1)
        {
            Invoke(nameof(newround),3f);
        }
    }

    private void newround()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

    }

}
