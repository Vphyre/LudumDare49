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
    Animator animator;
    bool facingRight;
    public bool dieScene;
    AudioSource sounds;
    public AudioClip walkSound;

    void Start()
    {
        body = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>(); 
        canGetItem = false;
        itemCaught = false;
        facingRight = true;
        sounds = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(!dieScene)
        {
            DropItem();
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical"); 
            GetItem();
            AnimationController();

            if((horizontal>0 && !facingRight) || (horizontal<0 && facingRight ))
            {
                Flip();
            }
            if(transform.childCount==1)
            {
                itemCaught = false;
            }
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Holding", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Dead", true);
        }        
    }

    private void FixedUpdate()
    {  
        if(!dieScene)
        {
            body.velocity = new Vector2(horizontal * speed, vertical * speed);
        }
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
        if(transform.childCount>1)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                transform.GetChild(1).gameObject.transform.position = new Vector3(itemPosition.position.x,itemPosition.position.y-0.3f,itemPosition.position.z);
                transform.GetChild(1).gameObject.transform.SetParent(null);
                itemObj = null;
                itemCaught = false;
                print("Dropou");
            }
        }
    }
    private void GetItem()
    {
        if(canGetItem && Input.GetKeyDown(KeyCode.E) && !itemCaught)
        {
            if(itemObj.tag == "Untagged")
            {
                itemCaught = false;
            }
            else
            {
                itemObj.transform.SetParent(transform);
                itemObj.transform.position = new Vector3(itemPosition.position.x,itemPosition.position.y, itemPosition.position.z);
                itemCaught = true;
            }
        }

    }
    void Flip()
    {
        facingRight =!facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180,0);
    }

    void AnimationController()
    {
        float walking;
        walking = Mathf.Abs(body.velocity.x)+Mathf.Abs(body.velocity.y);
        //Walking
        if(Mathf.Abs(walking)>0)
        {
            animator.SetFloat("Speed", Mathf.Abs(walking));
            animator.SetBool("Holding", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Dead", false);
        }
        //Idle
        else
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Holding", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Dead", false);
            sounds.Stop();
        }
        //Walking Holding
        if(walking>0 && itemCaught)
        {
            animator.SetFloat("Speed", Mathf.Abs(walking));
            animator.SetBool("Holding", true);
            animator.SetBool("Idle", false);
            animator.SetBool("Dead", false);
        }
        //Idle Holding
        else if(walking<=0 && itemCaught)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Holding", true);
            animator.SetBool("Idle", true);
            animator.SetBool("Dead", false);
            sounds.Stop();
        }
    }

    public void Sounds()
    {
        if(!sounds.isPlaying)
        {
            sounds.Play();
            sounds.clip = walkSound;
        }
    }

}
