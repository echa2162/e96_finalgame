using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{

    private float squareSize = 1f;
    [SerializeField] public GameObject woodBlock;
    [SerializeField] public GameObject woodPiece;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 18; i++)
        {
            CreatePiece(Random.Range(5.5f, 8.5f), Random.Range(-3f, 1.8f));
        }
    }


    void CreatePiece(float x, float y)
    {
        GameObject piece = Instantiate(woodPiece,new Vector2(x,y),Quaternion.identity,transform);
        GameObject block1 = InstantiateSquare(x,y);
        GameObject block2 = InstantiateSquare(x,y + squareSize);
        block2.transform.parent = piece.transform;
        block1.transform.parent = piece.transform;
        
    }

    GameObject InstantiateSquare(float x, float y)
    {
        Vector2 position = new Vector2(x, y);
        return Instantiate(woodBlock, position, Quaternion.identity, transform);
    }


}
