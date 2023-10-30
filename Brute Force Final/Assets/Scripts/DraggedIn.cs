using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggedIn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        DragAndDrop draggedObject = other.GetComponent<DragAndDrop>();
        draggedObject.setDraggable(false);
        other.transform.position = transform.position;
    }

}
