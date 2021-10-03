using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScreen : MonoBehaviour
{
    static float timer;
    public GameObject painel;
    // Start is called before the first frame update
    void Start()
    {
        timer = 5f;
        painel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if(timer<0)
        {
            painel.SetActive(false);
        }
    }
}
