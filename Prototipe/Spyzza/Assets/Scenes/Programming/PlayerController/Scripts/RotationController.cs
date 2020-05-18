using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{

    public float RotationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CamControl(); 
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -20, 50);

        transform.LookAt(Target);

        if (!player.GetComponent<ActionController>().isActionActive)
        {
            Target.rotation = Quaternion.Euler(-1 * mouseY / 2, mouseX, 0);
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
        
       
    }


}
