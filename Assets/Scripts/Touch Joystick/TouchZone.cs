using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;





public class TouchZone : MonoBehaviour, IPointerDownHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    [SerializeField] private JoyStick dragDrop;


    public void OnBeginDrag(PointerEventData eventData)
    {
        dragDrop.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragDrop.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragDrop.OnEndDrag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        //Vector2 pos = dragDrop.transform.TransformPoint(eventData.position);

        dragDrop.RectTransform.position = eventData.position;

        dragDrop.OnPointerDown(eventData);
      


    }
}
