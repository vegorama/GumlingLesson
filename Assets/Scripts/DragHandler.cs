using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public static GameObject cardBeingDragged;
    Vector2 startPosition;
    Quaternion startRotation;

    private GameManager_AnimalCards gamemanager;

    private void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager_AnimalCards>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cardBeingDragged = gameObject;
        startPosition = transform.position;
        startRotation = transform.rotation;

        //Play Sound
        if (gamemanager.canDrag == true)
        {
            gamemanager.cardShove.Play();
        }           
}

    public void OnDrag(PointerEventData eventData)
    {
        if (gamemanager.canDrag == true)
        {
            transform.position = new Vector2(Input.mousePosition.x, transform.position.y);
            transform.localRotation = Quaternion.Euler(0, 0, (startPosition.x - Input.mousePosition.x) / 10);
        }        
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("The x position : " + cardBeingDragged.transform.localPosition.x);

        if (cardBeingDragged.transform.localPosition.x < -280)
        {
            gamemanager.CardMovedLeft();
            //Debug.Log("Swiped LEFT");
        }
        else if (cardBeingDragged.transform.localPosition.x > 280)
        {
            gamemanager.CardMovedRight();
            //Debug.Log("Swiped RIGHT");
        }
        else
        {
            cardBeingDragged.transform.position = startPosition;
            cardBeingDragged.transform.rotation = startRotation;
            //Debug.Log("Swipe too short - RESET");
        }

        cardBeingDragged = null;
    }


}
