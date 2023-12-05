using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DraggedIn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        DisableAllDraggables();
        //  Make sure that player does not try to access to menu items at once
        other.transform.position = transform.position;
        // Snap the block into place 


        if (other.CompareTag("playButton"))
        {
            StartCoroutine(LoadLevel(2));
        } else if (other.CompareTag("optionsButton"))
        {
            StartCoroutine(LoadLevel(1));
        }


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


    IEnumerator LoadLevel(int levelIndex)
    {

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }
}
