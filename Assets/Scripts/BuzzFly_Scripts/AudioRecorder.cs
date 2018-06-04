using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecorder : MonoBehaviour {

    public AudioClip myAudioClip;
    public GameObject flyObject;

    private bool clipTimer;
    private float clipTime;

    void Start()
    {

    }

    void Update()
    {
        if (clipTimer)
        {
            clipTime += Time.deltaTime;
        }
    }

    public void StartRecord()
    {
        flyObject.GetComponent<AudioSource>().Stop();
        StopAllCoroutines();
        StartCoroutine(Record());
    }

    private IEnumerator Record()
    {
        yield return new WaitForSeconds(0.2f);

        myAudioClip = Microphone.Start(null, false, 10, 44100);
        clipTimer = true;
    }

    public void EndRecord()
    {
        clipTimer = false;
        if (clipTime > 10)
        {
            clipTime = 10;
        }

        //Assign the CLIPPED audio to the fly
        flyObject.GetComponent<FlyScript>().AssignSound(MakeSubclip(myAudioClip, 0, clipTime));

        clipTime = 0;

        flyObject.GetComponent<AudioSource>().Play();
    }

    private AudioClip MakeSubclip(AudioClip clip, float start, float stop)
    {
        /* Create a new audio clip */
        int frequency = clip.frequency;
        float timeLength = stop - start;
        int samplesLength = (int)(frequency * timeLength);
        AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);

        /* Create a temporary buffer for the samples */
        float[] data = new float[samplesLength];
        /* Get the data from the original clip */
        clip.GetData(data, (int)(frequency * start));
        /* Transfer the data to the new clip */
        newClip.SetData(data, 0);
        /* Return the sub clip */
        return newClip;
    }


    /*
     * Old Boilerplate code
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 60, 50), "Record"))
        {
            myAudioClip = Microphone.Start(null, false, 10, 44100);
            clipTimer = true;
        }

        if (GUI.Button(new Rect(10, 70, 60, 50), "Save"))
        {
            //SavWav.Save("Buzz_Recording", myAudioClip);

            clipTimer = false;

            //Assign the CLIPPED audio to the fly
            flyObject.GetComponent<FlyScript>().AssignSound( MakeSubclip(myAudioClip, 0, clipTime ));

            clipTime = 0;
        }
    }
    */
}
