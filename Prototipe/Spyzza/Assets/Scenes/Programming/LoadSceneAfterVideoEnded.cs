using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAfterVideoEnded : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component 
    public GameObject videoCanvas;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Cinematic")
        {
            VideoPlayer.loopPointReached += LoadSceneGame;
            Debug.Log("estoy en cinematic");
        }
        if (SceneManager.GetActiveScene().name == "Extras")
        {
            VideoPlayer.loopPointReached += LoadSceneMenu;
            Debug.Log("estoy en extras");
        }
    }
    public void LoadSceneGame(VideoPlayer vp)
    {
        SceneManager.LoadScene("Ricard Planta1", LoadSceneMode.Single);
        videoCanvas.SetActive(false);
    }

    public void LoadSceneMenu(VideoPlayer vp)
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        videoCanvas.SetActive(false);
    }
}
