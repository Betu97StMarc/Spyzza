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
    public GameObject menuCanvas;
    public GameObject inventaryCanvas;
    public GameObject alarmCanvas;
    public GameObject gameOverCanvas;
    public GameObject winCanvas;
    public Vector3 startPosition;
    public static bool alarmDisconnected;
    public static bool alarmActivated;
    public float alarmTimer = 120;
    public Text timerText;
    public Text interactText;
    public Light camera1L;
    public GameObject camera1B;
    public Light camera2L;
    public GameObject camera2B;
    public Light camera3L;
    public GameObject camera3B;
    public Light camera4L;
    public GameObject camera4B;
    public Toggle mug;
    public Toggle postIt;
    public Toggle redCard;
    public Toggle blueCard;
    public Toggle greenCard;
    public FindEM[] enemys;
    public bool isInside;



    // PRIVATE ATRIBUTES
    // private Vector3 startPosition;
    private string tag_collidingObject;
    private bool isThePlayerColliding;
    



    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector3(61, -13, 18);
        alarmDisconnected = false;
        //timerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        

        /*
         * 
         * Pruebas save data funciona
         * 
         * 
         */

        /* 
        if (Input.GetKey(KeyCode.K))
        {
            SaveSystem.SavePlayer(player);
        }

        if (Input.GetKey(KeyCode.L))
        {
            player.GetComponent<Player>().LoadPlayer();
        }

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
        */


        if (isThePlayerColliding)
        {
            interactText.enabled = true;
        }
        else
        {
            interactText.enabled = false;
        }

        if (Input.GetKey(KeyCode.E) && isThePlayerColliding)
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
            if (menuCanvas.activeSelf == false) gameOverCanvas.SetActive(true);
            mainCanvas.SetActive(false);
        }

        if (alarmDisconnected)
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

        if (camera1B.GetComponent<FieldOfView>().encontrado || camera2B.GetComponent<FieldOfView>().encontrado ||
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

        if (gameOverCanvas.activeSelf == true || menuCanvas.activeSelf == true || winCanvas.activeSelf == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public void UpdateUI()
    {
        if (menuCanvas.activeSelf == true)
        {
            mainCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            alarmCanvas.SetActive(false);
            inventaryCanvas.SetActive(false);
        }
        else
        {
            mainCanvas.SetActive(true);
        }

        timerText.text = alarmTimer.ToString("#.00");

        if (player.GetComponent<Player>().mug)
        {
            mug.isOn = true;
        }
        else
        {
            mug.isOn = false;
        }

        if (player.GetComponent<Player>().postIt)
        {
            postIt.isOn = true;
        }
        else
        {
            postIt.isOn = false;
        }

        if (player.GetComponent<Player>().redCard)
        {
            redCard.isOn = true;
        }
        else
        {
            redCard.isOn = false;
        }

        if (player.GetComponent<Player>().blueCard)
        {
            blueCard.isOn = true;
        }
        else
        {
            blueCard.isOn = false;
        }

        if (player.GetComponent<Player>().greenCard)
        {
            greenCard.isOn = true;
        }
        else
        {
            greenCard.isOn = false;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventaryCanvas.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            inventaryCanvas.SetActive(false);
        }
    }

    public void GoGame()
    {
        menuCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void GoMenu()
    {
        menuCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        winCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        Restart();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        player.transform.position = startPosition;
        alarmTimer = 120;
        alarmActivated = false;
        player.GetComponent<Player>().alive = true;
        alarmDisconnected = false;
        player.GetComponent<Player>().mug = false;
        player.GetComponent<Player>().postIt = false;
        player.GetComponent<Player>().redCard = false;
        player.GetComponent<Player>().blueCard = false;
        player.GetComponent<Player>().greenCard = false;
        player.GetComponent<ActionController>().mug.SetActive(true);
        player.GetComponent<Animator>().Play("Blend Tree Correr");
        player.GetComponent<Animator>().SetBool("Death", false);
        alarmCanvas.SetActive(false);
        winCanvas.SetActive(false);
        foreach (FindEM x in enemys)
        {
            x.catched = false;
            x.canSee = false;
            x.hasSeen = false;
            x.hearStop = true;
            x.Aggro = false;
            x.gameObject.transform.position = x.startPosition;
        }
    }

    public void GameOver()
    {
        player.GetComponent<Player>().alive = false;
        player.GetComponent<Animator>().SetBool ("Death",true);
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

    public void UpdateInteractPanel()
    {
        if (isThePlayerColliding)
        {
            interactText.enabled = true;
        }
        else
        {
            interactText.enabled = false;
        }
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

    public void ActivatePickUpMug()
    {
        SetTagCollidingObject("CogerTaza");
        CallAnalyseActionCollider();
    }

    public void ActivatePickUpPostIt()
    {
        SetTagCollidingObject("CogerPostIt");
        CallAnalyseActionCollider();
    }

    public void ActivatePickUpBlueCard()
    {
        SetTagCollidingObject("CogerAzul");
        CallAnalyseActionCollider();
    }

    public void ActivateFinalDoor()
    {
        SetTagCollidingObject("PuertaFinal");
        CallAnalyseActionCollider();
    }

    public void ActivateOffice()
    {
        SetTagCollidingObject("Taquilla");
        CallAnalyseActionCollider();
    }



    public void MessagePostIt()
    {
        Toast.Instance.Show("Safe password it's 8437", 2f, Toast.ToastColor.Dark);
    }


    public void MessageSafe()
    {
        Toast.Instance.Show("You don't know the password", 2f, Toast.ToastColor.Dark);
    }

    public void MessageCard()
    {
        Toast.Instance.Show("You don't have the security card's", 2f, Toast.ToastColor.Dark);
    }

    public void MessageAlarmDisconnected()
    {
        Toast.Instance.Show("Alarm system it's currently disabled", 2f, Toast.ToastColor.Dark);
    }

    public void MessageMugCollected()
    {
        Toast.Instance.Show("You already have the mug", 2f, Toast.ToastColor.Dark);
    }

    public void MessageRedCardCollected()
    {
        Toast.Instance.Show("You already have the red card", 2f, Toast.ToastColor.Dark);
    }

    public void MessageBlueCardCollected()
    {
        Toast.Instance.Show("You already have the blue card", 2f, Toast.ToastColor.Dark);
    }

    public void MessageGreenCardCollected()
    {
        Toast.Instance.Show("You already have the green card", 2f, Toast.ToastColor.Dark);
    }

    public void MessageFinalDoor()
    {
        Toast.Instance.Show("You need 3 security cards", 2f, Toast.ToastColor.Dark);
    }

}
