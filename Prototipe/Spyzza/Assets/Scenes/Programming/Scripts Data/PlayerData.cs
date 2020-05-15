using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public bool alive;
    public bool alarm;
    public bool mug;
    public bool postIt;
    public bool card1;
    public bool card2;
    public bool card3;
    public float[] position;

    /*
    * 
    * INFO: Método para almacenar los datos del player
    * 
    */
    public PlayerData(Player player)
    {
        //level = player.level;
        //health = player.health;
        alive = player.alive;
        alarm = player.alarm;
        mug = player.mug;
        postIt = player.postIt;
        card1 = player.card1;
        card2 = player.card2;
        card3 = player.card3;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
