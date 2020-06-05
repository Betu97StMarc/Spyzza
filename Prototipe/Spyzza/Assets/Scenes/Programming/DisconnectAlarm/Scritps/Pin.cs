using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isPinned = false;
    public float speed = 20f;    
    public float screenSize;
    public bool gameHasEnded;



    public Rigidbody2D rb;
    

    
    private void Update()
    {
        
        screenSize = Mathf.Max(Screen.width, Screen.height); 
        if (!isPinned)
        rb.MovePosition(rb.position + Vector2.up * screenSize * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Rotator")
        {
            if(FindObjectOfType<AlarmManager>().gameHasEnded == false)
            {
                transform.SetParent(col.transform);
                col.GetComponent<Rotator>().speed *= col.GetComponent<Rotator>().speedChanger;
                Score.PinCount++;
                isPinned = true;
                GameManager.Instance.buttonSound.Play();
            }
            else
            {
                transform.SetParent(col.transform);
                col.GetComponent<Rotator>().speed *= col.GetComponent<Rotator>().speedChanger;
                isPinned = true;
            }
            
        }else if(col.tag == "Pin")
        {
            FindObjectOfType<AlarmManager>().EndGame();
            GameManager.Instance.loseSound.Play();
        }
    }
}
