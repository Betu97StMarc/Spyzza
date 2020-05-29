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
        if (GameManager.Instance.sceneName == "Cinematic")
        {
            VideoPlayer.loopPointReached += LoadSceneGame;
        }
        if (GameManager.Instance.sceneName == "Extras")
        {
            VideoPlayer.loopPointReached += LoadSceneMenu;
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
