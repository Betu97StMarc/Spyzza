using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : Singleton<ActionController>
{
    public bool isActionActive;
    public bool isToastActive;
    public bool isSafe;
    private string tag_name_function;
    public float time_action;
    public  float time_change_location;
    public  float time_change_location_2;
    public  float time_toast;
    public  GameObject mug;
    public  GameObject blueCard;
    public  GameObject greenCard;
    private bool isShowedToastWindows;
    


    // Start is called before the first frame updatee
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
            if (isToastActive)
            {
                time_toast += Time.deltaTime;
                if(isSafe)
                {
                    if(time_toast >= 6)
                    {
                        Debug.Log("toast?");
                        GameManager.Instance.MessageRedCardCollected();
                        isToastActive = false;
                        isSafe = false;
                        time_toast = 0;
                        this.GetComponent<Player>().redCard = true;
                    }
                }
            }
            if (time_change_location <= 0)
            {
                
                transform.position = new Vector3(29.98f, -13, -11.49f);
            }


            if (time_change_location_2 <= 0)
            {
                transform.position = new Vector3(38.7f, -13, -10.83f);
            }

            if (time_toast <= 0)
            {
                time_toast = 0;
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

            

        }
    }

    public void StealGreenCard()
    {
        if (!this.GetComponent<Player>().greenCard)
        {
            this.GetComponent<Animator>().Play("CogerTaza");
            this.GetComponent<Player>().greenCard = true;
            time_action = 1;
            time_change_location = 20;
            time_change_location_2 = 20;
            isActionActive = true;
            greenCard.SetActive(false);
        }
        else
        {
            GameManager.Instance.MessageGreenCardCollected();
        }
    }

    public void AnalyseActionCollider(string tag_collider)
    {
        if (tag_collider == "CajaFuerte")
        {
            if (this.GetComponent<Player>().redCard == false)
            {
                if (this.GetComponent<Player>().postIt)
                {
                    this.GetComponent<Animator>().Play("Caja Fuerte");
                    time_action = 7;
                    isActionActive = true;
                    time_change_location = 20;
                    time_change_location_2 = 20;
                    time_toast = 0;
                    isToastActive = true;
                    isSafe = true;
                }
                else
                {
                    GameManager.Instance.MessageSafe();
                }
            }
            else
            {
                GameManager.Instance.MessageRedCardCollected();
            }
        }

        if (tag_collider == "EscalarReuniones")
        {
            this.GetComponent<Animator>().Play("Escalar");           
            time_action = 5;
            time_change_location = 5;
            time_change_location_2 = 15;
            isActionActive = true;            
        }

        if (tag_collider == "EscalarCajaFuerte")
        {
            this.GetComponent<Animator>().Play("Escalar");
            time_action = 5;
            time_change_location = 15;
            time_change_location_2 = 5;
            isActionActive = true;            
        }

        if (tag_collider == "CogerTaza")
        {
            if (!this.GetComponent<Player>().mug)
            {
                this.GetComponent<Animator>().Play("CogerTaza");
                this.GetComponent<Player>().mug = true;
                time_action = 1;
                time_change_location = 20;
                time_change_location_2 = 20;
                isActionActive = true;
                mug.SetActive(false);
            }
            else
            {
                GameManager.Instance.MessageMugCollected();
            }
        }

        if (tag_collider == "CogerPostIt")
        {
            if (!this.GetComponent<Player>().postIt)
            {
                this.GetComponent<Animator>().Play("CogerTaza");
                this.GetComponent<Player>().postIt = true;
                time_action = 1;
                time_change_location = 20;
                time_change_location_2 = 20;
                isActionActive = true;
                mug.SetActive(false);
            }
            else
            {
                GameManager.Instance.MessagePostIt();
            }
        }

        if (tag_collider == "CogerAzul")
        {
            if (!this.GetComponent<Player>().blueCard)
            {
                this.GetComponent<Animator>().Play("CogerTaza");
                this.GetComponent<Player>().blueCard = true;
                time_action = 1;
                time_change_location = 20;
                time_change_location_2 = 20;
                isActionActive = true;
                blueCard.SetActive(false);
            }
            else
            {
                GameManager.Instance.MessageBlueCardCollected();
            }
        }

        if (tag_collider == "PuertaFinal")
        {
            if (this.GetComponent<Player>().redCard && this.GetComponent<Player>().blueCard && this.GetComponent<Player>().greenCard)
            {
                this.GetComponent<Animator>().Play("IntroducirTarjeta");
                GameManager.Instance.winCanvas.SetActive(true);
                time_action = 3;
                time_change_location = 20;
                time_change_location_2 = 20;
                isActionActive = true;
                
            }
            else
            {
                GameManager.Instance.MessageFinalDoor();
            }
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
