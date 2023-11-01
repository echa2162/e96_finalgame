using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggedIn : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        DisableAllDraggables();
        //  Make sure that player does not try to access to menu items at once
        other.transform.position = transform.position;
        Debug.Log(transform.position);
        // Snap the block into place 

        //// TO DO: 
        //// Code to Change scene to corresponding block type

    }

    private void DisableAllDraggables()
    {
        // Find all objects with the DragAndDrop component in the scene
        DragAndDrop[] draggableObjects = FindObjectsOfType<DragAndDrop>();

        foreach (DragAndDrop draggable in draggableObjects)
        {
            draggable.setDraggable(false);
        }
    }

}
