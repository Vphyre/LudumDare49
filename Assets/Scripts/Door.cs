using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorPosition;

    public bool die;
    AudioSource sounds;
    public AudioClip doorSound;
    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(die)
            {
                Baby.GameOver();
            }
            else
            {
                other.gameObject.transform.position = new Vector3(doorPosition.position.x, doorPosition.position.y, doorPosition.position.z);
                sounds.PlayOneShot(doorSound);
            }
            
        }
    }
}
