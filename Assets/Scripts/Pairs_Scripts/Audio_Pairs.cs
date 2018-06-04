using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Pairs : MonoBehaviour {

    public AudioClip successSound;
    public AudioClip clickSound;
    public AudioClip yaySound;

    public void PlayCardSelectSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(clickSound);
    }

    public void PlayMatchSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(successSound);
    }

    public void PlayYaySound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(yaySound);
    }
}
