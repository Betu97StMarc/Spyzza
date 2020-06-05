using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject gameOne;
    public GameObject gameTwo;
    public GameObject gameThree;
    public GameObject winCanvas;
    public GameObject canvas;
    public GameObject newCamera;

    public Camera main;
    public int remaining = 3;
    public GameObject player;
    public GameObject playerMesh;
    public bool winGame;
    [Header("Panel1")]
    public bool wrongAnswerPanel1;
    public GameObject press11;
    public GameObject press12;
    public GameObject press13;
    [Header("Panel2")]
    public bool wrongAnswerPanel2;
    public GameObject press21;
    public GameObject press22;
    public GameObject press23;
    [Header("Panel3")]
    public bool wrongAnswerPanel3;
    public GameObject press31;
    public GameObject press32;
    public GameObject press33;
    [Header("Sounds")]
    public AudioSource wrongAnswer;
    public AudioSource correctAnswer;
    public AudioSource winSound;
    public GameObject roomSong;
    public GameObject fightSong;

    public GameObject[] vidaUP;
    public GameObject[] vidaDown;

    Scene scene;
    float timerBetweenAnswers = 1f;
    public bool first;
    bool second;
    public static bool FightStart = false;
    bool three;

    private void Start()
    {
        main.gameObject.SetActive(true);
        newCamera.SetActive(false);
        winGame = false;
        FightStart = false;
        winGame = false;
        roomSong.SetActive(true);
        fightSong.SetActive(false);
    }

    public void Update()
    {
        if (FightStart && !winGame)
        {
            fightSong.SetActive(true);
            roomSong.SetActive(false);
            this.GetComponent<Animator>().Play("Idle2");
            player.GetComponent<PlayerController>().enabled = false;
            playerMesh.SetActive(false);
            GameManager.Instance.mainCanvas.SetActive(false);
            main.gameObject.SetActive(false);
            newCamera.SetActive(true);
            scene = SceneManager.GetActiveScene();
            if (remaining > 0)
            {
                Debug.Log(remaining);
                if (first)
                {
                    canvas.SetActive(true);
                    gameOne.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        press13.SetActive(true);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        press12.SetActive(true);
                    }
                    if(Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        press11.SetActive(true);
                    }
                    if (Input.GetKeyUp(KeyCode.Alpha3))
                    {
                        correctAnswer.Play();
                        second = true;
                    }
                    if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Alpha1))
                    {
                        wrongAnswer.Play();
                        remaining--;
                        vidaUP[remaining].SetActive(false);
                        vidaDown[remaining].SetActive(true);
                        press13.SetActive(false);
                        press12.SetActive(false);
                        press11.SetActive(false);
                    }
                }
                if (second)
                {
                    timerBetweenAnswers -= Time.deltaTime;
                    first = false;
                    gameOne.SetActive(false);
                    gameTwo.SetActive(true);
                    if (timerBetweenAnswers <= 0)
                    {
                        if (Input.GetKeyDown(KeyCode.Alpha3))
                        {
                            press23.SetActive(true);
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha2))
                        {
                            press22.SetActive(true);
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            press21.SetActive(true);
                        }
                        if (Input.GetKeyUp(KeyCode.Alpha3))
                        {
                            three = true;
                            correctAnswer.Play();
                            timerBetweenAnswers = 1f;
                        }
                        if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Alpha1))
                        {
                            wrongAnswer.Play();
                            remaining--;
                            vidaUP[remaining].SetActive(false);
                            vidaDown[remaining].SetActive(true);
                            press23.SetActive(false);
                            press22.SetActive(false);
                            press21.SetActive(false);
                        }
                    }
                }
                if (three)
                {
                    timerBetweenAnswers -= Time.deltaTime;
                    second = false;
                    gameTwo.SetActive(false);
                    gameThree.SetActive(true);
                    if (timerBetweenAnswers <= 0)
                    {
                        if (Input.GetKeyDown(KeyCode.Alpha3))
                        {
                            press33.SetActive(true);
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha2))
                        {
                            press32.SetActive(true);
                        }
                        if (Input.GetKeyDown(KeyCode.Alpha1))
                        {
                            press31.SetActive(true);
                        }
                        if (Input.GetKeyUp(KeyCode.Alpha1))
                        {
                            winGame = true;
                            winSound.Play();
                            fightSong.SetActive(false);
                        }
                        if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Alpha3))
                        {
                            remaining--;
                            wrongAnswer.Play();
                            vidaUP[remaining].SetActive(false);
                            vidaDown[remaining].SetActive(true);
                            press33.SetActive(false);
                            press32.SetActive(false);
                            press31.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                main.gameObject.SetActive(true);
                newCamera.SetActive(false);
                SceneManager.LoadScene(scene.name);
            }
            
           
        }
        if (winGame)
        {
            gameThree.SetActive(false);
            winCanvas.SetActive(true);
            
            this.GetComponent<Animator>().Play("GameOver");
            for (int i = 0; i < vidaUP.Length; i++)
            {
                vidaUP[i].SetActive(false);
            }
            for (int i = 0; i < vidaUP.Length; i++)
            {
                vidaDown[i].SetActive(false);
            }
            if (Input.GetKey(KeyCode.B))
            {
                Debug.Log("W");
                GameManager.Instance.GoMenu();
            }
        }
    }
    public void BossEngine()
    {
        FightStart = true;
    }
}