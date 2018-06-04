using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelManager : MonoBehaviour {

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void LoadAnimalCards()
    {
        SceneManager.LoadScene("CardSwipe", LoadSceneMode.Single);
    }

    public void LoadBuzzFly()
    {
        SceneManager.LoadScene("FlyBuzz", LoadSceneMode.Single);
    }

    public void LoadPairs()
    {
        SceneManager.LoadScene("Pairs", LoadSceneMode.Single);
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

}
