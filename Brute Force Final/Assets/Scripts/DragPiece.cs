using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class DragPiece : MonoBehaviour
{

	public GameObject MasterBlock;
	GameObject[] AllBlocks;
	//The Parent object of each block will be what is technically transformed
	private bool dragging = false;
	private Vector3 offset;
    CameraShake cameraShake;
    AudioSource[] audios;
    public BoxCollider2D collision;
    Vector2 collisionSize;


    private int numberOfBlocks;
    private Vector3 originalPos;
	public bool occupyingSlot;
	public bool resetPos;
    private bool isRotating = false;
    public int breakCounter = 0;

    private void Start()
    {
        collision = GetComponent<BoxCollider2D>();
        collisionSize = collision.size;
        
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audios = GetComponents<AudioSource>();
        audios[0].time = 0.26f;
        audios[1].time = 0.15f;
        audios[2].time = 0.3f;
        audios[4].time = 0.15f;



        if (transform.parent != null)
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
        MasterBlock.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        foreach (var block in AllBlocks)
        {
            DragPiece Block = block.GetComponent<DragPiece>();
            Block.collision.size = collisionSize * 0.75f;
        }   
    }
    private void OnMouseUp()
    {
        MasterBlock.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        foreach (var block in AllBlocks)
        {
            DragPiece Block = block.GetComponent<DragPiece>();
            Block.collision.size = collisionSize;
        }
        dragging = false;

        audios[0].Play();

        GetComponent<AudioSource>().Play();


        foreach (var block in AllBlocks)
      {
            Collider2D blockCollider = block.GetComponent<Collider2D>();
            Collider2D[] colliders = Physics2D.OverlapBoxAll(block.transform.position, block.transform.localScale * 0.05f, 0);

            // Convert array to List for easier removal
            List<Collider2D> colliderList = new List<Collider2D>(colliders);

            // Remove the specific collider from the list
            colliderList.Remove(blockCollider);

            // Convert the List back to an array if necessary
            colliders = colliderList.ToArray();

            bool collisionWithWoodPiece = false;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("woodPiece") && collider.gameObject != MasterBlock)
                {
                    // Handle the collision with a wood piece here
                    MasterBlock.transform.position = originalPos;
                    audios[4].Play();
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
            audios[2].Play();

            //rotate the masterblock
            StartCoroutine(SmoothRotate(MasterBlock.transform, new Vector3(0, 0, 90), 0.3f));
        }
 }

    public void OnRotateRight()
    {

        if (dragging)
        {
            audios[2].Play();

            //rotate the masterblock
            StartCoroutine(SmoothRotate(MasterBlock.transform, new Vector3(0, 0, -90), 0.3f));
        }
    }

    public void OnBreakPiece()
    {
        if (dragging)
        {
            Debug.Log("Piece Break");
            StartCoroutine(cameraShake.Shake(0.1f, 0.3f));
            
            audios[1].Play();

            if(breakCounter >= numberOfBlocks * 4 - 1)
            {
                audios[3].Play();

                foreach (var block in AllBlocks)
                {
                    DragPiece dragPiece = block.GetComponent<DragPiece>();
                    Detach(block);
                    dragPiece.breakCounter = int.MinValue;
                    dragPiece.MasterBlock = block;
                    block.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    block.transform.position = new Vector3(block.transform.position.x 
                        + Random.Range(-0.05f, 0.05f), block.transform.position.y + Random.Range(-0.05f, 0.05f), block.transform.position.z);
                    dragPiece.AllBlocks = new GameObject[1];
                    dragPiece.AllBlocks[0] = block;
                }

            }
            else
            {
                breakCounter++;
            }
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

    public void Detach(GameObject child)
    {
        child.transform.parent = null;
    }


}
