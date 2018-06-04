using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyScript : MonoBehaviour {

    private float pitchLevel;
    private float panLevel;

    private float currentY;
    private float currentX;
    private float screenHeight;
    private float screenWidth;

    [Header("Fly To Centre")]
    public Vector2 target;
    public float speed = 100.0f;
    public bool isBeingDragged;

    private AudioSource buzzSound;

	// Use this for initialization
	void Start ()
    {
        //Get screen size
        screenHeight = Screen.height;
        screenWidth = Screen.width;
      
        //Set fly start point/target point
        target = transform.position;

        //Play sound
        buzzSound = GetComponent<AudioSource>();
        buzzSound.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Set Pitch and Pan depending on object transform
        SetPitch();

        //Return to centre when not being dragged
        if (!isBeingDragged)
        {
            FlyToCentre();
        }       
	}

    private void SetPitch()
    {
        currentY = transform.position.y / (screenHeight / 2);
        currentX = transform.position.x / (screenWidth / 2);

        //Bit of maths to  make the pitch go from +2 to -1
        if (currentY < 1)
        {
            pitchLevel = currentY;
        }
        else if (currentY > 1)
        {
            pitchLevel = currentY + ((currentY - 1) * 1.2f);
        }

        //Set pan level
        panLevel = currentX - 1;

        //Set pitch and pan on audiosource
        buzzSound.panStereo = panLevel;
        buzzSound.pitch = pitchLevel;
    }

    private void FlyToCentre()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void AssignSound(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;

        buzzSound.Stop();
        buzzSound.Play();
    }

}
