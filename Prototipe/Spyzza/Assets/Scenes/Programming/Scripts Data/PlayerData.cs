using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool bossLevel;
    public int health;
    public bool alive;
    public bool alarm;
    public bool mug;
    public bool postIt;
    public bool redCard;
    public bool blueCard;
    public bool greenCard;
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
        redCard = player.redCard;
        blueCard = player.blueCard;
        greenCard = player.greenCard;
        bossLevel = player.bossLevel;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
