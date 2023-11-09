using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int gridSizeX = 6;
    private int gridSizeY = 6;
    private float squareSize = 1.3f;

   [SerializeField] public GameObject squarePrefab;
   [SerializeField] public float xOffset,yOffset;
    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Vector2 offset = new Vector2(xOffset,yOffset);
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 spawnPosition = new Vector2(x * squareSize, y * squareSize) + offset ;
                Instantiate(squarePrefab, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
}