using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


[RequireComponent(typeof(Rigidbody2D))]
public class DragPiece : MonoBehaviour
{

	GameObject MasterBlock;
	GameObject[] AllBlocks;
	//The Parent object of each block will be what is technically transformed
	private bool dragging = false;
	private Vector3 offset;


    private int numberOfBlocks;
    private Vector3 originalPos;
	public bool occupyingSlot;
	public bool resetPos;
    private bool isRotating = false;

    private void Start()
    {


		if(transform.parent != null)
		{
			MasterBlock = transform.parent.gameObject;
		} else
		{
			MasterBlock = gameObject;
		}
		numberOfBlocks = MasterBlock.transform.childCount+1;
		//includes itself^^
		GetAllBlocks();
   
		
    }
    void Update() {
		if (dragging) {
			MasterBlock.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
		}

	}

	void GetAllBlocks()
	{
        // get the children of the masterblock and put it in AllBlocks
        AllBlocks = new GameObject[numberOfBlocks];
        AllBlocks[0] = MasterBlock;

        for (int i = 1; i <= MasterBlock.transform.childCount; i++)
        {
            AllBlocks[i] = MasterBlock.transform.GetChild(i - 1).gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
		//Debug.Log("Collided with " + collision.gameObject.name);
        if(gameObject == MasterBlock)
		{
			MasterBlock.transform.position = collision.transform.position;
		}

    }

    private void OnMouseDown()
    {
        offset = MasterBlock.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        originalPos = MasterBlock.transform.position;
    }
    private void OnMouseUp()
    {
        dragging = false;

      foreach (var block in AllBlocks)
      {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(block.transform.position, block.transform.localScale*0.15f, 0);
            bool collisionWithWoodPiece = false;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("woodPiece") && collider.gameObject.transform.parent != block.transform.parent && collider.gameObject != MasterBlock)
                {
                    // Handle the collision with a wood piece here
                    MasterBlock.transform.position = originalPos;
                    collisionWithWoodPiece = true;
                    break;
                }
            }

            if (collisionWithWoodPiece)
            {
                break;
            }
      }

    }


   public void OnRotateLeft()
{

        if (dragging)
        {
            //rotate the masterblock
            StartCoroutine(SmoothRotate(MasterBlock.transform, new Vector3(0, 0, 90), 0.3f));
        }
 }

    public void OnRotateRight()
    {

        if (dragging)
        {
            //rotate the masterblock
            StartCoroutine(SmoothRotate(MasterBlock.transform, new Vector3(0, 0, -90), 0.3f));
        }
    }

    IEnumerator SmoothRotate(Transform target, Vector3 angles, float duration)
    {
        if (!isRotating)
        {
            Debug.Log("Rotating");

            isRotating = true;
            Quaternion startRotation = target.rotation;
            Quaternion endRotation = Quaternion.Euler(target.eulerAngles + angles);
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                target.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
                yield return null;
            }
            target.rotation = endRotation;
            isRotating = false;
        }
    }

    public void DestroyPiece()
	{
		Destroy(MasterBlock);
	}
    public bool isDragging()
	{
		return dragging;
	}

    

}
