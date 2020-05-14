using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform body;
    public float rotY;
    public float RotationSpeed = 10f;
  
    // Start is called before the first frame update
    void Start()
    {
        body.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotación Cámara
        rotY += RotationSpeed * Time.deltaTime;
        body.transform.eulerAngles = new Vector3(0, rotY, 0);
        //Delimitación de la rotación
        if (rotY >= 50 || rotY <= -50)
        {
            RotationSpeed *= -1;
        }
        
        
    }
}
