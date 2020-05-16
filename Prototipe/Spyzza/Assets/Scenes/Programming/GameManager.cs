using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : Singleton<GameManager>
{
    // PUBLIC ATRIBUTES
    public Player player;
    public GameObject mainCanvas;
    public GameObject inventaryCanvas;
    public GameObject alarmCanvas;
    public GameObject gameOverCanvas;
    public static bool alarmDisconnected;
    public static bool alarmActivated;
    public float alarmTimer = 120;
    public Text timerText;
    public Light camera1L;
    public GameObject camera1B;
    public Light camera2L;
    public GameObject camera2B;
    public Light camera3L;
    public GameObject camera3B;
    public Light camera4L;
    public GameObject camera4B;




    // PRIVATE ATRIBUTES
    private Vector3 startPosition;
    private string tag_collidingObject;
    private bool isThePlayerColliding;
    


    // Start is called before the first frame update
    void Start()
    {
        alarmDisconnected = false;
        //timerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        timerText.text = alarmTimer.ToString("#.00");
        
        /*
         * 
         * Pruebas save data funciona
         * 
         * */
        
        /* 
        if (Input.GetKey(KeyCode.K))
        {
            SaveSystem.SavePlayer(player);
        }

        if (Input.GetKey(KeyCode.L))
        {
            player.GetComponent<Player>().LoadPlayer();
        }
        */

       

        if (Input.GetKey(KeyCode.L))
        {
            if (player.GetComponent<Player>().alive)
            {
                player.GetComponent<Player>().alive = false;
            }
            else
            {
                player.GetComponent<Player>().alive = true;
            }

        }

        if (Input.GetKey(KeyCode.E))
        {
            CallAnalyseActionCollider();
        }

        if (player.GetComponent<Player>().alive)
        {
            gameOverCanvas.SetActive(false);
            mainCanvas.SetActive(true);
        }
        else
        {
            gameOverCanvas.SetActive(true);
            mainCanvas.SetActive(false);
        }

        if(alarmDisconnected)
        {
            camera1L.enabled = false;       
            camera2L.enabled = false;
            camera3L.enabled = false;
            camera4L.enabled = false;
            alarmTimer = 120;
            
        }
        else
        {
            camera1L.enabled = true;
            camera2L.enabled = true;
            camera3L.enabled = true;
            camera4L.enabled = true;
            
        }

        if(camera1B.GetComponent<FieldOfView>().encontrado || camera2B.GetComponent<FieldOfView>().encontrado || 
            camera3B.GetComponent<FieldOfView>().encontrado || camera4B.GetComponent<FieldOfView>().encontrado)
        {
            alarmActivated = true;
        }

        if (alarmActivated && !alarmDisconnected)
        {
            alarmTimer -= Time.deltaTime;
            timerText.enabled = true;
        }
        else
        {
            timerText.enabled = false;
            alarmTimer = 120;
        }

        if (alarmTimer <= 0)
        {
            GameOver();
            timerText.enabled = false;
        }

        if(gameOverCanvas.activeSelf == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
    }

    public void Restart()
    {
        player.GetComponent<Player>().LoadPlayer();
        alarmTimer = 120;
        alarmActivated = false;
        player.GetComponent<Player>().alive = true;
        alarmDisconnected = false;
    }

    public void GameOver()
    {
        player.GetComponent<Player>().alive = false;
    }

    public void setIsThePlayerColliding(bool new_state)
    {
        this.isThePlayerColliding = new_state;
    }

    public string GetTagCollidingObject()
    {
        return tag_collidingObject;
    }

    public void SetTagCollidingObject(string tag)
    {
        tag_collidingObject = tag;
    }

    public void CallAnalyseActionCollider()
    {
        ActionController.Instance.AnalyseActionCollider(tag_collidingObject);
    }

    public void ActivateDisconnectAlarm()
    {
        SetTagCollidingObject("DisconnectAlarm");
        CallAnalyseActionCollider();
    }

    public void ActivateSafe()
    {
        SetTagCollidingObject("CajaFuerte");
        CallAnalyseActionCollider();
    }

    public void ActivateClimb()
    {
        SetTagCollidingObject("EscalarReuniones");
        CallAnalyseActionCollider();
    }

    public void ActivateClimbSafe()
    {
        SetTagCollidingObject("EscalarCajaFuerte");
        CallAnalyseActionCollider();
    }

    public void MessagePostIt()
    {
        Toast.Instance.Show("Contraseña caja fuerte 8437", 2f, Toast.ToastColor.Dark);
    }


    public void MessageSafe()
    {
        Toast.Instance.Show("No sabes la contraseña", 2f, Toast.ToastColor.Dark);
    }

    public void MessageCard()
    {
        Toast.Instance.Show("No tienes las targetas", 2f, Toast.ToastColor.Dark);
    }

    public void MessageAlarmDisconnected()
    {
        Toast.Instance.Show("El sistema de alarma ya está desactivado", 2f, Toast.ToastColor.Dark);
    }


}
