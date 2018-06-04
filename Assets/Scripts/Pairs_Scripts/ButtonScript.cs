using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    [Header("References")]
    GameObject GameManagerRef;
    GameManager_Pairs gameManager;

    [Header("Settings")]
    public Button button;
    public GameObject cardImage;
    Text buttonText;
    

    // Use this for initialization
    void Start ()
    { 
        GameManagerRef = GameObject.Find("GameManager");
        gameManager = GameManagerRef.GetComponent<GameManager_Pairs>();

        buttonText = button.GetComponentInChildren<Text>();
        HideCard();
    }

    public void CardBackColour()
    {
        buttonText.color = new Color(0, 0, 0, 0);
        cardImage.SetActive(false);

    }

    public void HideCard()
    {
        buttonText.color = new Color(0, 0, 0, 0);
        cardImage.SetActive(false);
    }

    private void ShowCard()
    {
        buttonText.color = new Color(0, 0, 0, 1);
        cardImage.SetActive(true);
    }

    public void FlipClick()
    {       
            //Flip cards
            if (gameManager.cardText1 == "")
            {
                gameManager.card1 = button;
                gameManager.cardText1 = button.GetComponentInChildren<Text>().text;

                ShowCard();
            }
            else if (gameManager.cardText2 == "")
            {
                gameManager.card2 = button;

                if (gameManager.card1 == gameManager.card2)
                {
                    //Do nothing
                    gameManager.card2 = null;
                }
                else
                {           
                    gameManager.cardText2 = button.GetComponentInChildren<Text>().text;

                    button.GetComponentInChildren<Text>().enabled = true;

                    ShowCard();

                    StartCoroutine(gameManager.CardCheck());
                }
            }
        }

    }
