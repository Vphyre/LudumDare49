using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SpriteRenderer>().color = new Color(.6f,.6f,.6f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            transform.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        }
    }
    protected void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            transform.GetComponent<SpriteRenderer>().color = new Color(.6f,.6f,.6f,1f);
        }
    }

}
