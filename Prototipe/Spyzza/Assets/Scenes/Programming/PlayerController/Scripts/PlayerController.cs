using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float speed = 1f;
    public bool canJump;
    public bool isCrouched;    
    public Animator playerC;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        playerC.SetFloat("Horizontal", x);
        playerC.SetFloat("Vertical", y);
        playerC.SetFloat("Speed", speed);

        if(!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            speed = 3;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            speed = 2;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
        {
            speed = 1;
        }
        


        if (!this.GetComponent<ActionController>().isActionActive && this.GetComponent<Player>().alive)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);              
            }

            if (Input.GetKey(KeyCode.S))

                transform.Translate(-1 * Vector3.forward * Time.deltaTime * speed);

            if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector3.left * Time.deltaTime * speed);

            if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector3.right * Time.deltaTime * speed);

            if (Input.GetButtonDown("Jump") && canJump && speed > 2.5f)
            {
                canJump = false;
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 250f, 0));
                playerC.SetTrigger("Saltar");
            }
        }
       
            }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            canJump = true;
        }
    }
}
    

       
