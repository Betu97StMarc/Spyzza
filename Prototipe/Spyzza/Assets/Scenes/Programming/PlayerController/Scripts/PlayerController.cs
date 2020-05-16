using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
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

        if (!this.GetComponent<ActionController>().isActionActive)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    speed = 1f;
                }
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                speed = 2.5f;
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            else if (!Input.GetKey(KeyCode.LeftControl) && (!Input.GetKey(KeyCode.LeftShift)))
            {
                speed = 1.9f;
            }


            if (Input.GetKey(KeyCode.S))

                transform.Translate(-1 * Vector3.forward * Time.deltaTime * speed);

            if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector3.left * Time.deltaTime * speed);

            if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector3.right * Time.deltaTime * speed);

            if (Input.GetButtonDown("Jump") && canJump)
            {
                canJump = false;
                gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 250f, 0));
                playerC.Play("Jump");
            }
        }
       
        /*if(Input.GetKey(KeyCode.LeftControl) && !isCrouched)
        {
            isCrouched = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftControl) && isCrouched)
        {
            isCrouched = false;
        }
        if(isCrouched)
        {
            playerC.SetTrigger("Agacharse");
        }
        else
        {
            playerC.SetTrigger("Levantarse");
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            canJump = true;
        }
    }
}
    

       
