using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baby : MonoBehaviour
{
    public string[] items;
    string[] originalItems;
    public string[] babyQuotes;
    public int amountRequested;
    public float randomTime = 20f;
    public float moodTime;
    float moodTimeAux;
    public string[] levelRequests;
    GameObject[] itemPlaces;
    GameObject[] babyPlaces;
    string itemRequest;
    int sort;
    public bool canChangePlace;
    public Text itemText;
    public Text moodNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        itemPlaces = GameObject.FindGameObjectsWithTag("Place");
        babyPlaces = GameObject.FindGameObjectsWithTag("BabyPlaces");
        levelRequests = new string[amountRequested];
        originalItems = new string[10];
        sort = 0;
        canChangePlace = false;
        moodTimeAux = moodTime;
        CopyArray();
        SortItemsRequests();
        ResetArray();
        SortItemPlaces();
    }

    // Update is called once per frame
    void Update()
    {
        randomTime-=Time.deltaTime;
        RandomizeBaby();
        BabyMood();
    }
    void SortItemsRequests()
    {
        do
        {
            float aux;
            do
            {
                aux = (int)Random.Range(0,9.9f);
                levelRequests[sort] = items[(int)aux];
            }
            while(levelRequests[sort] == null);
            items[(int)aux] = null;
            // print(levelRequests[sort]);
            sort++;        
        }
        while(sort<levelRequests.Length);
    }
    void SortItemPlaces()
    {
        int aux = 0;
        do
        {
            int sort;
            do
            {
                sort = (int)Random.Range(0,17.9f);
            }
            while(itemPlaces[sort].transform.childCount==1);
            IntantiateItem(items[aux],itemPlaces[sort]);
            aux++;
        }
        while(aux<10);
    }
    void ResetArray()
    {
        int resetArray = 0;
        do
        {
            items[resetArray] = originalItems[resetArray];
            // print(items[resetArray]);
            resetArray++;
        }
        while(resetArray<10);
    }
    void CopyArray()
    {
        int copyArray = 0;
        do
        {
            originalItems[copyArray] = items[copyArray];
            copyArray++;
        }
        while(copyArray<10);
    }
    void IntantiateItem(string nameItem, GameObject place)
    {
        var obj = Instantiate(Resources.Load("Prefabs/Testes/Items/"+nameItem, typeof(GameObject)) as GameObject);
        obj.transform.position = new Vector3(place.transform.position.x, place.transform.position.y, place.transform.position.z);
        obj.transform.SetParent(place.transform);
    }
    void RandomizeBaby()
    {
        //Baby's Place
        if(randomTime<0 && canChangePlace)
        {
            int placeSort = (int)Random.Range(0,6.9f);
            transform.position = new Vector3(babyPlaces[placeSort].transform.position.x,babyPlaces[placeSort].transform.position.y, babyPlaces[placeSort].transform.position.z);
            randomTime = 20f;
        }
    }
    void BabyMood()
    {
        float percent;
        moodTime-=Time.deltaTime;
        percent = (moodTime*100f)/moodTimeAux;
        moodNumber.text = percent.ToString("F");
    }
    void BabyRequest()
    {
        
    }
}
