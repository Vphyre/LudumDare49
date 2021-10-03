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
    public int itemsCaught;
    int itensRemaining;
    public float randomTime = 20f;
    public float randomRequestTime = 0f;
    public float cryTime = 20f;
    public float moodTime;
    float moodTimeAux;
    float moodDrain = 1f;
    public string[] levelRequests;
    public string currentRequest;
    GameObject[] itemPlaces;
    GameObject[] babyPlaces;
    string itemRequest;
    int sort;
    int requestSort;
    public Text itemText;
    public Text moodNumber;
    public Text remainingText;
    public bool canChangePlace;
    public bool canRandomizeRequest;
    public bool canCry;
    public bool canRandomizeItensPlace;
    
    // Start is called before the first frame update
    void Start()
    {
        itemPlaces = GameObject.FindGameObjectsWithTag("Place");
        babyPlaces = GameObject.FindGameObjectsWithTag("BabyPlaces");
        levelRequests = new string[amountRequested];
        originalItems = new string[10];
        sort = 0;
        requestSort = 0;
        itemsCaught = 0;
        canChangePlace = false;
        moodTimeAux = moodTime;
        itensRemaining = amountRequested;
        CopyArray();
        SortItemsRequests();
        ResetArray();
        SortItemPlaces();        
    }

    // Update is called once per frame
    void Update()
    {
        randomTime-=Time.deltaTime;
        randomRequestTime-=Time.deltaTime;
        cryTime-=Time.deltaTime;
        RandomizeBaby();
        BabyMood();
        BabyRequest();
        Cry();
        GameBehaviour();
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
        obj.name = nameItem;
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
        moodTime-=Time.deltaTime*moodDrain;
        percent = (moodTime*100f)/moodTimeAux;
        moodNumber.text = percent.ToString("F");
    }
    void BabyRequest()
    {
        if(canRandomizeRequest && randomRequestTime<0)
        {
            requestSort = (int)Random.Range(0,(float)amountRequested-0.1f);
            if(levelRequests[requestSort] == "BabyBottle")
            {
                itemText.text = babyQuotes[0];
                currentRequest = levelRequests[requestSort];
            }
            else if (levelRequests[requestSort] == "Ball")
            {
                itemText.text = babyQuotes[1];
                currentRequest = levelRequests[requestSort];
            }
            else if (levelRequests[requestSort] == "ChildSDrawing")
            {
                itemText.text = babyQuotes[2];
                currentRequest = levelRequests[requestSort];
            }
            else if (levelRequests[requestSort] == "Chocolate")
            {
                itemText.text = babyQuotes[3];
                currentRequest = levelRequests[requestSort];
            }
            else if (levelRequests[requestSort] == "ColoredPencils")
            {
                itemText.text = babyQuotes[4];
                currentRequest = levelRequests[requestSort];
            }
            else if (levelRequests[requestSort] == "GameDice")
            {
                itemText.text = babyQuotes[5];
                currentRequest = levelRequests[requestSort];
            }
            else if (levelRequests[requestSort] == "LittleBear")
            {
                itemText.text = babyQuotes[6];
                currentRequest = levelRequests[requestSort];
            }
            else if (levelRequests[requestSort] == "LittleBook")
            {
                itemText.text = babyQuotes[7];
                currentRequest = levelRequests[requestSort];
            }
            else if (levelRequests[requestSort] == "Pacifier")
            {
                itemText.text = babyQuotes[8];
                currentRequest = levelRequests[requestSort];
            }
            else if (levelRequests[requestSort] == "Rattle")
            {
                itemText.text = babyQuotes[9];
                currentRequest = levelRequests[requestSort];
            }
            randomRequestTime = 5f;
        }
        else if(!canRandomizeRequest)
        {
            if(levelRequests[itemsCaught] == "BabyBottle")
            {
                itemText.text = babyQuotes[0];
                currentRequest = levelRequests[itemsCaught];
            }
            else if (levelRequests[itemsCaught] == "Ball")
            {
                itemText.text = babyQuotes[1];
                currentRequest = levelRequests[itemsCaught];
            }
            else if (levelRequests[itemsCaught] == "ChildSDrawing")
            {
                itemText.text = babyQuotes[2];
                currentRequest = levelRequests[itemsCaught];
            }
            else if (levelRequests[itemsCaught] == "Chocolate")
            {
                itemText.text = babyQuotes[3];
                currentRequest = levelRequests[itemsCaught];
            }
            else if (levelRequests[itemsCaught] == "ColoredPencils")
            {
                itemText.text = babyQuotes[4];
                currentRequest = levelRequests[itemsCaught];
            }
            else if (levelRequests[itemsCaught] == "GameDice")
            {
                itemText.text = babyQuotes[5];
                currentRequest = levelRequests[itemsCaught];
            }
            else if (levelRequests[itemsCaught] == "LittleBear")
            {
                itemText.text = babyQuotes[6];
                currentRequest = levelRequests[itemsCaught];
            }
            else if (levelRequests[itemsCaught] == "LittleBook")
            {
                itemText.text = babyQuotes[7];
                currentRequest = levelRequests[itemsCaught];
            }
            else if (levelRequests[itemsCaught] == "Pacifier")
            {
                itemText.text = babyQuotes[8];
                currentRequest = levelRequests[itemsCaught];
            }
            else if (levelRequests[itemsCaught] == "Rattle")
            {
                itemText.text = babyQuotes[9];
                currentRequest = levelRequests[itemsCaught];
            }
        }
    }
    void Cry()
    {
        if(canCry && cryTime<0)
        {
            int crySort = (int)Random.Range(0,3.9f);;
            if(crySort==0)
            {
                moodDrain = 1f;
            }
            else if(crySort==2)
            {
                moodDrain = 1f;
            }
            else if(crySort == 3)
            {
                moodDrain = 3f;
                print("Chorando");
            }
            cryTime = 5f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Item"))
        {
            if(other.gameObject.name==currentRequest)
            {
                itemsCaught++;
                itensRemaining--;
                print("OBRIGADO");
                Destroy(other.gameObject);
            }
            else
            {
                print("GAMEOVER");
            }
        }
    }
    void GameBehaviour()
    {
        remainingText.text = itensRemaining.ToString();
    }

}
