using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotStatus : MonoBehaviour
{
    private bool isOccupied = false;
    private bool isHovered = false;

    GameObject[] pieces;

    private void Start()
    {
        pieces = GameObject.FindGameObjectsWithTag("woodPiece");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("WOW");

        isOccupied = true;
        Debug.Log("WOW");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOccupied = false;
    }

    public bool isOccupying() {  return isOccupied; }
    public bool isHovering() { return isHovered; }

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
