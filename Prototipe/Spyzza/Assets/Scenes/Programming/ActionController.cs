using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : Singleton<ActionController>
{
    public bool isActionActive;
    private string tag_name_function;
    private float time_action;
    public  float time_change_location;
    public  float time_change_location_2;
    private bool isShowedToastWindows;
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActionActive)
        {
            time_action -= Time.deltaTime;
            time_change_location -= Time.deltaTime;
            time_change_location_2 -= Time.deltaTime;
            if (time_change_location <= 0)
            {
                Debug.Log("xq no cambio");
                transform.position = new Vector3(29.98f, -13, -11.49f);
            }

            if (time_change_location_2 <= 0)
            {
                transform.position = new Vector3(38.7f, -13, -10.83f);
            }

            if (time_action <= 0)
            {
                time_action = 0;
                isActionActive = false;
            }

            if (time_change_location <= -2)
            {
                time_change_location = 0;
            }

            if (time_change_location_2 <= -2)
            {
                time_change_location_2 = 0;
            }

            if (tag_name_function == "CajaFuerte")
            {
                ActionCajaFuerte();
            }
            else if(tag_name_function == "DisconnectAlarm")
            {
                ActionDisconnectAlarm();
            }
            else if (tag_name_function == "EscalarReuniones")
            {
                ActionDisconnectAlarm();
                
            }
            else if (tag_name_function == "EscalarCajaFuerte")
            {
                ActionDisconnectAlarm();
                
            }

        }
    }

    public void AnalyseActionCollider(string tag_collider)
    {
        if (tag_collider == "CajaFuerte")
        {
            if(this.GetComponent<Player>().postIt)
            {
                this.GetComponent<Animator>().Play("Caja Fuerte");
                time_action = 6;
                isActionActive = true;

            }
            else
            {
                GameManager.Instance.MessageSafe();
            }
        }

        if (tag_collider == "EscalarReuniones")
        {
            this.GetComponent<Animator>().Play("Escalar");
            time_action = 6;
            time_change_location = 5;
            time_change_location_2 = 10;
            isActionActive = true;
            
        }

        if (tag_collider == "EscalarCajaFuerte")
        {
            this.GetComponent<Animator>().Play("Escalar");
            time_action = 6;
            time_change_location = 10;
            time_change_location_2 = 5;
            isActionActive = true;
            
        }

        if (tag_collider == "DisconnectAlarm")
        {
            //GameManager.Instance.alarmCanvas.SetActive(true);
            if(!GameManager.alarmDisconnected)
            {
                GameManager.Instance.alarmCanvas.SetActive(true);
                
            }
            else
            {
                GameManager.Instance.MessageAlarmDisconnected();
                
            }
        }
    }

    public void ActionCajaFuerte()
    {
        /*GameObject player = GameManager.Instance.GetPlayer();
        GameObject fakePlayer = GameManager.Instance.GetFakePlayer(GameManager.Instance.GetTagCollidingObject());
        if (isActionActive)
        {
            // TO DO ACTIVAR EL FAKE PLAYER

            fakePlayer.SetActive(true);
            player.GetComponent<Player>().setThePlayerIsInAnAction(true);
            GameManager.Instance.setIsThePlayerColliding(false);
            GameManager.Instance.UpdateInteractPanel();
            player.SetActive(false);
        }
        else
        {
            player.GetComponent<Player>().setThePlayerIsInAnAction(false);
            player.SetActive(true);


            // TODO DESACTIVAR EL FAKE PLAYER
            fakePlayer.SetActive(false);

        }*/
    }

    public void ActionDisconnectAlarm()
    {
        
    }
}
