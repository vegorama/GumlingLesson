using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Pairs : MonoBehaviour
{
    [Header("Banners and Menus")]
    public GameObject endGameMenu;
    public GameObject pauseMenu;

    [Header("Game Variables")]
    public Button card1;
    public Button card2;
    public string cardText1;
    public string cardText2;
    public int score;

    [Header("Audio Ref")]
    public Audio_Pairs audioManager;

    [Header("Game Text")]
    public Text infoText;
    [SerializeField]
    private Text scoreText;

    [Header("Button Lists")]
    public GameObject[] buttonList;
    [SerializeField]
    public string[] wordList;
    [SerializeField]
    private Sprite[] animalImageList;
    [SerializeField]
    private AudioClip[] animalSounds;
    private Dictionary<string, AudioClip> audioClips;


    // Use this for initialization
    private void Start()
    {
        AudioDictionary();
        RandomiseCards();
        AssignCardValues();
    }

    private void AudioDictionary()
    {
        audioClips = new Dictionary<string, AudioClip>();

        audioClips.Add("Bird", animalSounds[0]);
        audioClips.Add("Cat", animalSounds[1]);
        audioClips.Add("Dog", animalSounds[2]);
        audioClips.Add("Elephant", animalSounds[3]);
        audioClips.Add("Frog", animalSounds[4]);
        audioClips.Add("Zebra", animalSounds[5]);
        audioClips.Add("Lion", animalSounds[6]);
        audioClips.Add("Monkey", animalSounds[7]);
    }

    private void RandomiseCards()
    {
        wordList = new string[] { "Bird", "Bird", "Cat", "Cat", "Dog", "Dog", "Elephant", "Elephant", "Frog", "Frog", "Zebra", "Zebra", "Lion", "Lion", "Monkey", "Monkey" };

        // Knuth shuffle algorithm
        for (int t = 0; t < wordList.Length; t++)
        {
            string tmp = wordList[t];
            int r = Random.Range(t, wordList.Length);
            wordList[t] = wordList[r];
            wordList[r] = tmp;
        }
    }

    private void AssignCardValues()
    {
        //Assign text
        for (int t = 0; t < buttonList.Length; t++)
        {
            if (buttonList[t] != null)
            {
                buttonList[t].GetComponentInChildren<Text>().text = wordList[t];
            }
        }

        //Assign Sprites
        for (int t = 0; t < buttonList.Length; t++)
        {
            if (buttonList[t] != null)
            {
                if (buttonList[t].GetComponentInChildren<Text>().text == "Bird")
                {
                    buttonList[t].GetComponentsInChildrenNoParent<Image>()[0].sprite = animalImageList[0];
                }
                else if (buttonList[t].GetComponentInChildren<Text>().text == "Cat")
                {
                    buttonList[t].GetComponentsInChildrenNoParent<Image>()[0].sprite = animalImageList[1];
                }
                else if (buttonList[t].GetComponentInChildren<Text>().text == "Dog")
                {
                    buttonList[t].GetComponentsInChildrenNoParent<Image>()[0].sprite = animalImageList[2];
                }
                else if (buttonList[t].GetComponentInChildren<Text>().text == "Elephant")
                {
                    buttonList[t].GetComponentsInChildrenNoParent<Image>()[0].sprite = animalImageList[3];
                }
                else if (buttonList[t].GetComponentInChildren<Text>().text == "Frog")
                {
                    buttonList[t].GetComponentsInChildrenNoParent<Image>()[0].sprite = animalImageList[4];
                }
                else if (buttonList[t].GetComponentInChildren<Text>().text == "Zebra")
                {
                    buttonList[t].GetComponentsInChildrenNoParent<Image>()[0].sprite = animalImageList[5];
                }
                else if (buttonList[t].GetComponentInChildren<Text>().text == "Lion")
                {
                    buttonList[t].GetComponentsInChildrenNoParent<Image>()[0].sprite = animalImageList[6];
                }
                else if (buttonList[t].GetComponentInChildren<Text>().text == "Monkey")
                {
                    buttonList[t].GetComponentsInChildrenNoParent<Image>()[0].sprite = animalImageList[7];
                }
            }
        }
    }

    private void AwardPoint()
    {
        score++;
        scoreText.text = "Score : " + score.ToString();
    }

    private void UpdateText(string text)
    {
        infoText.text = text;
    }

    private void PlaySound(string animalText)
    {
        GetComponent<AudioSource>().PlayOneShot(audioClips[animalText]);
    }

    private void HideCardsChecker()
    {
        card1.GetComponent<ButtonScript>().CardBackColour();
        card2.GetComponent<ButtonScript>().CardBackColour();
    }

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    //Card Check system
    public IEnumerator CardCheck()
    {
        //Debug.Log("CardCheck being called!");

        if ((cardText1 != string.Empty) && (cardText1 != string.Empty))
        {
            //A match
            if (cardText1 == cardText2)
            {
                UpdateText(cardText1);

                yield return new WaitForSeconds(.5f);
                audioManager.PlayMatchSound();
                yield return new WaitForSeconds(.5f);

                PlaySound(cardText1);
                yield return new WaitForSeconds(2f);

                Destroy(card1.gameObject);
                Destroy(card2.gameObject);               

                cardText1 = string.Empty;
                cardText2 = string.Empty;
                card1 = null;
                card2 = null;
              
                AwardPoint();

                if (score >= 8)
                {
                    audioManager.PlayYaySound();
                    endGameMenu.SetActive(true);
                }
            }

            //Not a match
            else
            {
                yield return new WaitForSeconds(.5f);

                HideCardsChecker();

                cardText1 = string.Empty;
                cardText2 = string.Empty;
                card1 = null;
                card2 = null;
            }
        }
    }
}