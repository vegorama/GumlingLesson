using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_BuzzFly : MonoBehaviour {

    public GameObject pauseMenu;
    private GameObject buzzFly;

    // Use this for initialization
    void Start()
    {
        buzzFly = GameObject.Find("BuzzFly");
    }

    public void ShowPauseMenu()
    {
        buzzFly.GetComponent<AudioSource>().Stop();
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        buzzFly.GetComponent<AudioSource>().Play();
        pauseMenu.SetActive(false);
    }

}
