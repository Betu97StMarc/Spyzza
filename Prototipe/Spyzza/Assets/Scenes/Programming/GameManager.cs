using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : Singleton<GameManager>
{
    // PUBLIC ATRIBUTES
    public Player player;
    public GameObject objectPlayer;
    public GameObject mainCanvas;
    public GameObject menuCanvas;
    public GameObject optionsCanvas;
    public GameObject pauseCanvas;
    public bool gamePaused;
    public GameObject inventaryCanvas;
    public GameObject alarmCanvas;
    public GameObject gameOverCanvas;
    public GameObject winCanvas;
    public GameObject laser1;
    public GameObject laser2;
    public GameObject laser3;
    public Vector3 startPosition;
    public bool alarmDisconnected;
    public bool alarmActivated;
    public float alarmTimer = 120;
    public Text timerText;
    public GameObject timerBackground;
    public Text interactText;
    public GameObject interactBackground;
    public Light camera1L;
    public GameObject camera1B;
    public Light camera2L;
    public GameObject camera2B;
    public Light camera3L;
    public GameObject camera3B;
    public Light camera4L;
    public GameObject camera4B;
    public Button continueButton;
    public bool mug;
    public bool postIt;
    public bool redCard;
    public bool blueCard;
    public bool greenCard;
    public FindEM[] enemys;
    public bool isInside;
    public Scene currentScene;
    public string sceneName;



    // PRIVATE ATRIBUTES
    // private Vector3 startPosition;
    private string tag_collidingObject;
    private bool isThePlayerColliding;




    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "Ricard Planta1")
        {
            player.level = 1;
            startPosition = new Vector3(61, -13, 18);
            alarmDisconnected = false;
            //timerText.enabled = false;
            SaveSystem.SavePlayer(player);
        }

        if (sceneName == "Boss Ricard")
        {
            player.level = 2;
            objectPlayer.transform.position = new Vector3(-0.5f, 0.15f, 3.1f);
            SaveSystem.SavePlayer(player);
        }
    }

    // Update is called once per frame
    void Update()
    {

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
        if (sceneName == "Menu" || sceneName == "Cinematic" || sceneName == "Extras")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (Input.GetKey(KeyCode.E) && isThePlayerColliding)
            {
                CallAnalyseActionCollider();
            }
            /*if(player.level == 2)
            {
                continueButton.interactable = true;
            }
            else if(player.level == 1)
            {
                continueButton.interactable = false;
            }*/
        }

       

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            UpdatePause();
        }

        if (sceneName == "Ricard Boss")
        {
            //LOGICA SALA BOSS
            if(winCanvas.activeSelf == true)
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
        if (sceneName == "Ricard Planta1")
        {
            UpdatePause();
            UpdateLaser();
            UpdateUI();
            UpdateInteractPanel();

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
                timerBackground.SetActive(true);
            }
            else
            {
                timerText.enabled = false;
                timerBackground.SetActive(false);
                alarmTimer = 120;
            }

            if (alarmTimer <= 0)
            {
                GameOver();
                timerText.enabled = false;
                timerBackground.SetActive(false);
            }


        }
    }
    public void UpdateLaser()
    {
        if (!alarmDisconnected)
        {
            laser1.SetActive(true);
            laser2.SetActive(true);
            laser3.SetActive(true);
        }
        else
        {
            laser1.SetActive(false);
            laser2.SetActive(false);
            laser3.SetActive(false);
        }
    }

    public void GoBoss()
    {
        SceneManager.LoadScene("Ricard Boss", LoadSceneMode.Single);
    }

    public void UpdateUI()
    {

        if (sceneName == "Ricard Planta1" || sceneName == "Boss Ricard")
        {
            if (gameOverCanvas.activeSelf == true || winCanvas.activeSelf == true || alarmCanvas.activeSelf == true || pauseCanvas.activeSelf == true)
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
        else if (sceneName == "Menu" || sceneName == "Cinematic" || sceneName == "Extras")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        timerText.text = alarmTimer.ToString("#.0 s");

        if (sceneName == "Boss Ricard")
        {
            //LOGICA SALA BOSS
        }

            if (player.GetComponent<Player>().mug)
        {
            mug = true;
        }
        else
        {
            mug = false;
        }

        if (player.GetComponent<Player>().postIt)
        {
            postIt = true;
        }
        else
        {
            postIt = false;
        }

        if (player.GetComponent<Player>().redCard)
        {
            redCard = true;
        }
        else
        {
            redCard = false;
        }

        if (player.GetComponent<Player>().blueCard)
        {
            blueCard = true;
        }
        else
        {
            blueCard = false;
        }

        if (player.GetComponent<Player>().greenCard)
        {
            greenCard = true;
        }
        else
        {
            greenCard = false;
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
        SceneManager.LoadScene("Cinematic", LoadSceneMode.Single);
        menuCanvas.SetActive(false);
    }

    public void GoExtras()
    {
        SceneManager.LoadScene("Extras", LoadSceneMode.Single);
        menuCanvas.SetActive(false);
    }

    public void GoMenu()
    {
        /*menuCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        winCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        Restart();*/
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void GoOptions()
    {
        //menuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
    }

    public void CloseOptions()
    {
        //menuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;
    }

    public void UpdatePause()
    {
        if(gamePaused)
        {
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<Animator>().enabled = false;
            player.GetComponentInChildren<RotationController>().enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<Animator>().enabled = true;
            player.GetComponentInChildren<RotationController>().enabled = true;
        }
    }

    public void ClosePause()
    {
        gamePaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        /*player.transform.position = startPosition;
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
            x.hearStop = false;
            x.Aggro = false;
            x.gameObject.transform.position = x.startPosition;
        }*/
        SceneManager.LoadScene("Ricard Planta1", LoadSceneMode.Single);
    }

    public void GameOver()
    {
        player.GetComponent<Player>().alive = false;
        player.GetComponent<Animator>().SetBool("Death", true);

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
            interactBackground.SetActive(true);
        }
        else
        {
            interactText.enabled = false;
            interactBackground.SetActive(false);

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

    public void ActivateScreens()
    {
        SetTagCollidingObject("Pantalles");
        CallAnalyseActionCollider();
    }

    public void ActivateOffice()
    {
        SetTagCollidingObject("Taquilla");
        CallAnalyseActionCollider();
    }

    public void ActivateBossFight()
    {
        SetTagCollidingObject("BossCollider");
        CallAnalyseActionCollider();
    }



    public void MessagePostIt()
    {
        Toast.Instance.Show("La contraseña de la caja fuerte es: 8437", 2f, Toast.ToastColor.Dark);
    }


    public void MessageSafe()
    {
        Toast.Instance.Show("No sabes la contraseña", 2f, Toast.ToastColor.Dark);
    }

    public void MessageCard()
    {
        Toast.Instance.Show("No tienes todas las tarjetas de seguridad", 2f, Toast.ToastColor.Dark);
    }

    public void MessageAlarmDisconnected()
    {
        Toast.Instance.Show("El sistema de alarmas ya está desactivado", 2f, Toast.ToastColor.Dark);
    }

    public void MessageMugCollected()
    {
        Toast.Instance.Show("Ya tienes la taza", 2f, Toast.ToastColor.Dark);
    }

    public void MessageRedCardCollected()
    {
        Toast.Instance.Show("Ya tienes la tarjeta roja", 2f, Toast.ToastColor.Dark);
    }

    public void MessageBlueCardCollected()
    {
        Toast.Instance.Show("Ya tienes la tarjeta azul", 2f, Toast.ToastColor.Dark);
    }

    public void MessageGreenCardCollected()
    {
        Toast.Instance.Show("Ya tienes la tarjeta verde", 2f, Toast.ToastColor.Dark);
    }

    public void MessageFinalDoor()
    {
        Toast.Instance.Show("Necesitas las tres tarjetas de seguridad", 2f, Toast.ToastColor.Dark);
    }

    public void MessageScreens()
    {
        Toast.Instance.Show("AL RAIT! APAGUEM PANTALLAS!", 4f, Toast.ToastColor.Dark);
    }

}
