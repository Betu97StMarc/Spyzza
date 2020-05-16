using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    
    public float rotZ;
    public float RotationSpeed = 25f;
  
    // Start is called before the first frame update
    void Start()
    {
        //transform.eulerAngles = new Vector3(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotación Cámara
        rotZ += RotationSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, rotZ, 0);
        
        
       
        //Delimitación de la rotación
        if (rotZ >= 50|| rotZ <= -50)
        {
            RotationSpeed *= -1;
        }       
    }
}
