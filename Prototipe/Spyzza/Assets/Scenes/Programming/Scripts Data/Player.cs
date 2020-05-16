using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool alive;
    public bool alarm;
    public bool mug;
    public bool postIt;
    public bool card1;
    public bool card2;
    public bool card3;
    //public Text xT;
    //public Text yT;
    //public Text zT;
    public static Vector3 position;
    public static Vector3 startPosition;

    public void Awake()
    {
        alive = true;
        alarm = false;
        mug = false;
        postIt = false;
        card1 = false;
        card2 = false;
        card3 = false;
        
    }
    public void Update()
    {
        //xT.text = gameObject.transform.position.x.ToString();
        //yT.text = gameObject.transform.position.y.ToString();
        //zT.text = gameObject.transform.position.z.ToString();
        position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DisconnectAlarm")
        {
            GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
        }

        if (other.tag == "EscalarReuniones")
        {
            GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
        }

        if (other.tag == "EscalarCajaFuerte")
        {
            GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
        }

        if (other.tag == "CajaFuerte")
        {
            GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
        }

        if (other.tag == "Rumba")
        {
            alive = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DisconnectAlarm")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
        }

        if (other.tag == "EscalarReuniones")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
        }

        if (other.tag == "EscalarCajaFuerte")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
        }

        if (other.tag == "CajaFuerte")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
        }
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

        //level = data.level;
        //health = data.health;

        
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }


    

}
