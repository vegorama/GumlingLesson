using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlyDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private GameObject flyObject;
    Vector2 startPosition;

    private void Start()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        flyObject = gameObject;
        startPosition = flyObject.transform.position;

        flyObject.GetComponent<FlyScript>().isBeingDragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        flyObject.GetComponent<FlyScript>().isBeingDragged = false;

        flyObject = null;
    }


}
