using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlotStatus : MonoBehaviour
{
    private bool isOccupied = false;

    GameObject[] pieces;

    private void Start()
    {
        pieces = GameObject.FindGameObjectsWithTag("woodPiece");
    }


    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<DragPiece>().isDragging() == false)
        {
            isOccupied = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("LEFT");
        isOccupied = false;
    }

    public bool isOccupying() {  return isOccupied; }
    public bool anyDragged()
    {
        foreach (var piece in pieces)
        {
            if(piece.GetComponent<DragPiece>().isDragging())
            {
                return true;
            }
        }

        return false;
    }



}
