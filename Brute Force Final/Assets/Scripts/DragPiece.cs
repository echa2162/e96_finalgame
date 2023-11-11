using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class DragPiece : MonoBehaviour
{

	int numSquares;
	private bool dragging = false;
	private Vector3 offset;

    private void Start()
    {
        numSquares = transform.childCount;
    }
    void Update() {
		if (dragging) {
			transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
		}



	}
	
    private void OnMouseDown() {
		offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
		dragging = true;

	}

	private void OnMouseUp() {
		dragging = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		Debug.Log("WOW");
    }



    public bool isDragging()
	{
		return dragging;
	}



    
}
