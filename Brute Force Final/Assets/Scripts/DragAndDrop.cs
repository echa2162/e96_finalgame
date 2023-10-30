using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour , IDragHandler
{
    public bool draggable = true;
    private RectTransform draggingObjectRectTransform;
    private RectTransform dragHereRectTransform;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float dampingSpeed = .00005f;

   
    private void Awake()
    {
        draggingObjectRectTransform = transform as RectTransform;

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (draggable)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObjectRectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
            {
                draggingObjectRectTransform.position = Vector3.SmoothDamp(draggingObjectRectTransform.position, globalMousePosition, ref velocity, dampingSpeed);
            }
        }
    }

    public void setDraggable(bool value)
    {
        draggable = value;
    }
    
}
