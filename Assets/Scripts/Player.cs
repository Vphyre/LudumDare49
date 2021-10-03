using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    public float speed;
    public Transform itemPosition;
    bool canGetItem;
    bool itemCaught;
    GameObject itemObj;

    void Start ()
    {
        body = GetComponent<Rigidbody2D>(); 
        canGetItem = false;
        itemCaught = false;
    }

    void Update ()
    {
        DropItem();
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        GetItem();
    }

    private void FixedUpdate()
    {  
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            canGetItem = true;
            itemObj = other.gameObject;
            print(other.gameObject.name);
            print(itemObj.name);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            canGetItem = true;
            itemObj = other.gameObject;
            // print(other.gameObject.name);
            // print(itemObj.name);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            canGetItem = false;
            itemObj = null;
        }
    }
    private void DropItem()
    {
        if(transform.childCount>2)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                transform.GetChild(2).gameObject.transform.position = new Vector3(itemPosition.position.x,itemPosition.position.y-0.3f,itemPosition.position.z);
                transform.GetChild(2).gameObject.transform.SetParent(null);
                itemObj = null;
                itemCaught = false;
            }
        }
    }
    private void GetItem()
    {
        if(canGetItem && Input.GetKeyDown(KeyCode.E) && !itemCaught)
        {
            itemObj.transform.SetParent(transform);
            itemObj.transform.position = new Vector3(itemPosition.position.x,itemPosition.position.y, itemPosition.position.z);
            itemCaught = true;
        }
    }
}
