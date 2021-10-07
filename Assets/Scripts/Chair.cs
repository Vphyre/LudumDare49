using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    protected bool canClimb;
    protected GameObject player;
    protected Transform seat;

    // Start is called before the first frame update
    void Start()
    {
        canClimb = false;
        seat = transform.GetChild(0).gameObject.transform;
        transform.GetComponent<SpriteRenderer>().color = new Color(.6f,.6f,.6f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        Climb();
        GoDown();
    }
    protected void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            canClimb = true;
            player = other.gameObject;
            if(gameObject.tag != "Untagged")
            {
                transform.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
            }
        }
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            canClimb = false;
            player = null;
            transform.GetComponent<SpriteRenderer>().color = new Color(.6f,.6f,.6f,1f);
        }
    }

    protected void Climb()
    {
        if(canClimb && Input.GetKeyUp(KeyCode.Q))
        {
            player.transform.SetParent(transform);
            player.transform.position = new Vector3(seat.position.x, seat.position.y, seat.position.z);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.tag = "Untagged";
            transform.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        }
    }
    protected void GoDown()
    {
        if(transform.childCount>1 && Input.GetKey(KeyCode.S))
        {
            player.transform.position = new Vector3(transform.position.x, transform.position.y-0.65f, seat.position.z);
            player.transform.SetParent(null);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            gameObject.tag = "Item";
        }

    }
}
