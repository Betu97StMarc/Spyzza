using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int level;
    public int health;
    public Text xT;
    public Text yT;
    public Text zT;
    public Text levelT;
    public Text healthT;

    /*
    * 
    * INFO: Método para aumentar el level
    * 
    */
    public void MoreLevel()
    {
       level++;
    }
    /*
    * 
    * INFO: Método para aumentar el health
    * 
    */
    public void MoreHealth()
    {
        health++;
    }

    /*
    * 
    * INFO: Método para guardar player
    * 
    */
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    /*
    * 
    * INFO: Método para cargar player
    * 
    */
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }


    public void Update()
    {
        xT.text = gameObject.transform.position.x.ToString();
        yT.text = gameObject.transform.position.y.ToString();
        zT.text = gameObject.transform.position.z.ToString();
        levelT.text = level.ToString();
        healthT.text = health.ToString();
    }

}
