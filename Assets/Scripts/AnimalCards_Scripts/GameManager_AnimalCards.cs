using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_AnimalCards : MonoBehaviour {

    [Header("Private")]
    private Text Letter1text;
    private Text Letter2text;
    private Text ScoreText;
    private GameObject stackSpawnPoint;
    public GameObject endGameBanner;

    private List<Card> cardsInfo = new List<Card>();
    private List<GameObject> GameCards = new List<GameObject>();
    private AudioSource cardMove;   

    [Header("UI")]
    public int childScore;

    [Header("Logic")]
    public int cardsRemaining;
    public GameObject currentCard;
    public string currentLetter;
    public bool canDrag = true;
    public bool rightIsRight;

    [Header("Audio Manager")]
    public Audio_Pairs audioManager;

    [Header("References")]
    public Sprite[] animalImages;
    public AudioClip[] animalSounds;
    public GameObject cardPrefabRef;
    public AudioSource cardShove;

    // Use this for initialization
    void Start()
    {
        Letter1text = GameObject.Find("Letter1").GetComponentInChildren<Text>();
        Letter2text = GameObject.Find("Letter2").GetComponentInChildren<Text>();
        ScoreText = GameObject.Find("ScoreText").GetComponentInChildren<Text>();
        cardMove = GetComponent<AudioSource>();
        cardShove = GetComponents<AudioSource>()[1];
        //endGameBanner = GameObject.Find("EndGameBanner");
        stackSpawnPoint = GameObject.Find("CardStackPoint");

        //Create Card list, Shuffle it, Instantiate Cards
        CreateCardList();
        InstantiateStack();
        
        //Count number of cards in stack and cardsRemaining counter
        cardsRemaining = cardsInfo.Count;

        //Begin Main Logic
        GameLogic();
    }

    private void GameLogic()
    {
        if (cardsRemaining > 0)
        {
            ActivateCard();
            PlayCardAudio();

            //Randomly set letters left or right
            SetLetters();
        }
        else
        {
            audioManager.PlayYaySound();
            endGameBanner.SetActive(true);        
        }
    }

    private void CreateCardList()
    {
        for (int i = 0; i < animalImages.Length; i++)
        {
            cardsInfo.Add(new Card(animalImages[i].name, animalImages[i], animalSounds[i]));
        }

        //And shuffle this stack
        cardsInfo.Shuffle();
    }

    private void InstantiateStack()
    {
        for (int i = 0; i < cardsInfo.Count; i++)
        {
            //Instantiate card
            GameObject newCard = Instantiate(cardPrefabRef, stackSpawnPoint.transform);

            //Set Card Image (using i+1 because of List, and specifiying CHILDREN with [1])
            newCard.GetComponentsInChildren<Image>()[2].sprite = cardsInfo[i].cardImage;
            //Set Card Text
            newCard.GetComponentInChildren<Text>().text = cardsInfo[i].cardID;
            //Set Card Sound
            newCard.GetComponent<AudioSource>().clip = cardsInfo[i].cardSound;
            //Set Name
            newCard.name = "Card " + (i + 1);
            //Set position in Hierarchy
            newCard.transform.SetAsFirstSibling();
            //Add to List of gameObjects
            GameCards.Add(newCard);

            //Offset position
            newCard.transform.localPosition = new Vector3(newCard.transform.localPosition.x - (35 * i), newCard.transform.localPosition.y - (10 * i), newCard.transform.localPosition.z);

        }

        //Reverse Order
        GameCards.Reverse();
    }

    private void SetLetters()
    {
        rightIsRight = ExtensionMethods.randomBoolean();

        if (rightIsRight)
        {
            Letter2text.text = currentLetter;
            Letter1text.text = ExtensionMethods.RandomLetter();
        }
        else
        {
            Letter1text.text = currentLetter;
            Letter2text.text = ExtensionMethods.RandomLetter();
        }
        //Recur is Letters are the same. (TODO - hacky??)
        if (Letter1text.text == Letter2text.text)
        {
            SetLetters();
        }
    }

    private void ActivateCard()
    {
        //Set CurrentCard to bottom of List
        currentCard = stackSpawnPoint.transform.GetChild(cardsRemaining - 1).gameObject;
        //Set Opacity
        Color visible = new Color(1, 1, 1, 1);

        currentCard.GetComponentsInChildren<Image>()[1].color = visible;

        //Set Current Letter
        currentLetter = currentCard.GetComponentInChildren<Text>().text.Substring(0, 1);

        //Set draggable by activating script
        currentCard.GetComponent<DragHandler>().enabled = true;
    }

    private void TurnEnd()
    {
        //Update Score
        ScoreText.text = "Score : " + childScore;

        //Remove used card
        cardsRemaining--;
        GameCards.RemoveAt(cardsRemaining);            
        Destroy(currentCard);

        //Start move cards Coroutine - calls GameLogic() on end.
        StartCoroutine( MoveCardsAlong() );    
    }

    private IEnumerator MoveCardsAlong()
    {
        //This way round concertinas the cards from back to front instead
        /*
        //Move cards animation
        foreach (GameObject card in GameCards)
        {
            card.transform.localPosition = new Vector3((card.transform.localPosition.x + 25), card.transform.localPosition.y, card.transform.localPosition.z);

            yield return new WaitForSeconds(0.1f);
        }        */

        canDrag = false;

        //TODO understand this! - Still not 100% why its needs to be i - 1. Some 0 indexing pizzazz.
        for (int i = GameCards.Count; i > 0; i--)
        {
            yield return new WaitForSeconds(0.15f);

            GameObject card = GameCards[i - 1];

            card.transform.localPosition = new Vector3((card.transform.localPosition.x + 35), (card.transform.localPosition.y + 10), card.transform.localPosition.z);
            cardMove.Play();
         
        }

        canDrag = true;

        //Call next step
        GameLogic();
    }

    //Public Functions

    public void CardMovedLeft()
    {
        if (rightIsRight)
        {
            //Wrong
        }
        else
        {
            //Right
            childScore++;
            audioManager.PlayMatchSound();
        }
        TurnEnd();
    }

    public void CardMovedRight()
    {
        if (rightIsRight)
        {
            //Right
            childScore++;
            audioManager.PlayMatchSound();
        }
        else
        {
            //Wrong            
        }
        TurnEnd();
    }

    public void PlayCardAudio()
    {
        if (currentCard)
        {
            currentCard.GetComponent<AudioSource>().Play();
        }       
    }

}
