using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    bool canJump;
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


        if (Input.GetKey(KeyCode.W))
        {
            
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
                   
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 0.5f;
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                speed = 1;
            }
            
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
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000f, 0));
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
    

       
