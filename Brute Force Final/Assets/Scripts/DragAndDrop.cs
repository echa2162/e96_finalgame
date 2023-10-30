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

   // This implementation uses Unity's UI System and IDragHandler
    private void Awake()
    {
        draggingObjectRectTransform = transform as RectTransform;

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (draggable) //easy way to disable dragging
        {
            // gets mouses position on screen relative to camera I believe
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObjectRectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePosition))
            {
                // SmoothDamp just makes the transition less abrupt, closer damingSpeed is to zero, the faster the dragged object follows
                draggingObjectRectTransform.position = Vector3.SmoothDamp(draggingObjectRectTransform.position, globalMousePosition, ref velocity, dampingSpeed);
            }
        }
    }

    public void setDraggable(bool value)
    {
        draggable = value;
        //allow DraggedIn to change value of draggable objects
    }
    
}
