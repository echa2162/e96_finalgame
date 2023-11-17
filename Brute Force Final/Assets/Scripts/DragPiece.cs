using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
public class DragPiece : MonoBehaviour
{

	GameObject MasterBlock;
	//The Parent object of each block will be what is technically transformed
	private int numSquares;
	private bool dragging = false;
	private Vector3 offset;

    private void Start()
    {
		if(transform.parent != null)
		{
			MasterBlock = transform.parent.gameObject;
		} else
		{
			MasterBlock = gameObject;
		}

        numSquares = MasterBlock.transform.childCount + 1;
		// All the children of masterblock + itself
    }
    void Update() {
		if (dragging) {
			MasterBlock.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
		}

	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(gameObject == MasterBlock)
		{
			MasterBlock.transform.position = collision.transform.position;
		}
    }

    private void OnMouseDown() {
		offset = MasterBlock.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
		dragging = true;

	}

	private void OnMouseUp() {
		dragging = false;

	}



    public bool isDragging()
	{
		return dragging;
	}

    

}
