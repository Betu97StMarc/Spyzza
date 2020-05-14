using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 180f;
    public float speedChanger = -1.15f;

    // Update is called once per frame
    void Update()
    {
        if(speed <= -350)
        {
            speed = -350;
        }
        else if(speed >= 350)
        {
            speed = 350;
        }
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
    }
}
