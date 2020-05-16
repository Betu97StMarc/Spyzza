using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    public bool gameHasEnded = false;
    public Rotator rotator;
    public Spawner spawner;
    public GameObject[] pins;
    public GameObject canvasGameOver;
    public GameObject canvasWin;
    public GameObject canvasAlarm;
    //public Animator animatorCamera;
    

    public void Update()
    {
        pins = GameObject.FindGameObjectsWithTag("Pin");
        if(!gameHasEnded)
        {
            canvasGameOver.SetActive(false);
            canvasWin.SetActive(false);
        }
        
        if(Score.PinCount >= 3)
        {
            WinGame();
        }

        if(gameHasEnded)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void EndGame()
    {
        if (gameHasEnded)
        {
            return;
        }
        rotator.enabled = false;
        spawner.enabled = false;
        //animatorCamera.SetTrigger("EndGame");
        gameHasEnded = true;
        canvasGameOver.SetActive(true);
    }

    public void RestartGame()
    {
        gameHasEnded = false; ;
        rotator.enabled = true;
        spawner.enabled = true;
        rotator.speed = 180;
        GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
        Destroy(pin);
        //animatorCamera.SetTrigger("RestartGame");
        Score.PinCount = 0;      
    }

    public void BackGame()
    {
        gameHasEnded = false; ;
        rotator.enabled = true;
        spawner.enabled = true;
        rotator.speed = 180;
        GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
            Destroy(pin);
        //animatorCamera.SetTrigger("RestartGame");
        Score.PinCount = 0;
        canvasAlarm.SetActive(false);
    }

    public void WinGame()
    {
        if (gameHasEnded)
        {
            return;
        }
        rotator.enabled = false;
        spawner.enabled = false;
        //animatorCamera.SetTrigger("WinGame");
        gameHasEnded = true;
        canvasWin.SetActive(true);
        GameManager.alarmDisconnected = true;
        GameManager.alarmActivated = false;
    }
}
