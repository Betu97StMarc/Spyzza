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
    public bool redCard;
    public bool blueCard;
    public bool greenCard;
    public LayerMask enemyMask;
    //public Text xT;
    //public Text yT;
    //public Text zT;
    public static Vector3 position;
    public static Vector3 startPosition;
    private Collider[] enemyCollider;

    public void Awake()
    {
        alive = true;
        alarm = false;
        mug = false;
        postIt = false;
        redCard = false;
        blueCard = false;
        greenCard = false;
        
    }
    public void Update()
    {
        //xT.text = gameObject.transform.position.x.ToString();
        //yT.text = gameObject.transform.position.y.ToString();
        //zT.text = gameObject.transform.position.z.ToString();
        position = transform.position;
        enemyCollider = Physics.OverlapSphere(gameObject.transform.position, 3, enemyMask);

        if (enemyCollider.Length > 0)
        {
            Debug.Log("s'ha torbat un enemic");
            if (Input.GetKey(KeyCode.E))
            {
                Steal();
            }
        }
        
       
    }

    public void Steal()
    {
        for (int i = 0; i < enemyCollider.Length; i++)
        {
            FindEM x = enemyCollider[i].GetComponent<FindEM>();
            if (x.holdsObject && !x.canSee)
            {
                if (!this.GetComponent<Player>().greenCard)
                {
                    this.GetComponent<Animator>().Play("CogerTaza");
                    this.GetComponent<Player>().greenCard = true;
                    this.GetComponent<ActionController>().time_action = 1;
                    this.GetComponent<ActionController>().time_change_location = 20;
                    this.GetComponent<ActionController>().time_change_location_2 = 20;
                    this.GetComponent<ActionController>().isActionActive = true;
                    this.GetComponent<ActionController>().greenCard.SetActive(false);
                }
                else
                {
                    GameManager.Instance.MessageGreenCardCollected();
                }
            }
        }
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

        if (other.tag == "CogerTaza")
        {
            GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
        }

        if (other.tag == "CogerPostIt")
        {
            GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
        }

        if (other.tag == "CogerAzul")
        {
            GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
        }

        if (other.tag == "PuertaFinal")
        {
            GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
        }

        if (other.tag == "Pantalles")
        {
            GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
        }

        if (other.tag == "BossCollider")
        {
            GameManager.Instance.winCanvas.SetActive(true);
            
            /*GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
            Debug.Log("Im in");*/
        }

        if (other.tag == "Taquilla")
        {
            GameManager.Instance.setIsThePlayerColliding(true);
            GameManager.Instance.SetTagCollidingObject(other.tag);
        }

        if (other.tag == "Rumba")
        {
            GameManager.Instance.GameOver();
        }

        if (other.tag == "Laser1")
        {
            GameManager.Instance.alarmActivated = true;
        }

        if (other.tag == "Laser2")
        {
            GameManager.Instance.alarmActivated = true;
        }

        if (other.tag == "Laser3")
        {
            GameManager.Instance.alarmActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DisconnectAlarm")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
        }

        if (other.tag == "EscalarReuniones")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
        }

        if (other.tag == "EscalarCajaFuerte")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
        }

        if (other.tag == "CajaFuerte")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
        }

        if (other.tag == "CogerTaza")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
        }

        if (other.tag == "CogerPostIt")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
        }

        if (other.tag == "CogerAzul")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
        }

        if (other.tag == "PuertaFinal")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
        }

        if (other.tag == "Pantalles")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
        }

        if (other.tag == "BossCollider")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
            Debug.Log("Im out");
        }

        if (other.tag == "Taquilla")
        {
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
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
