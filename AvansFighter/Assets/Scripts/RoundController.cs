using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{

    public HitDetection player;
    public HitDetection enemy;
    public RoundTimer timer;

    public int playerHealth;
    public int enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.getHealth();
        enemyHealth = enemy.getHealth();
        Debug.Log(timer.getCurrentTime());
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.getHealth();
        enemyHealth = enemy.getHealth();
        checkKnockoutWin();
        Debug.Log(timer.getCurrentTime());
        if (timer.currentTime < 1)
        {
            checkTimerWin();
        }

    }

    public void checkTimerWin()
    {
        if(playerHealth > enemyHealth)
        {
            selectWinner("Ken");
        }
        else if(enemyHealth > playerHealth)
        {
            selectWinner("Ryu");
        }
    }

    private void checkKnockoutWin()
    {
        if(enemyHealth < 1)
        {
            selectWinner("Ken");
        }
        else if(playerHealth < 1)
        {
            selectWinner("Ryu");
        }
    }

    private void selectWinner(string character)
    {
        Debug.Log(character + " has won the game");
    }
}
