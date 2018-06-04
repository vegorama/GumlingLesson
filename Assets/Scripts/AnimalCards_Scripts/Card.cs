using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card  {

    public string cardID;
    public Sprite cardImage;
    public AudioClip cardSound;

    public Card(string _id, Sprite _cardImage)
    {
        cardID = _id;
        cardImage = _cardImage;
    }

    public Card(string _id, Sprite _cardImage, AudioClip _cardSound)
    {
        cardID = _id;
        cardImage = _cardImage;
        cardSound = _cardSound;
    }

}
