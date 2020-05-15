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
    public GameObject gameOverCanvas;



    // PRIVATE ATRIBUTES
    private Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * 
         * Pruebas save data funciona
         * 
         * */
        
         
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
    }

    public void Restart()
    {
        player.GetComponent<Player>().LoadPlayer();
    }

    
}
