using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GridManager : MonoBehaviour
{
    private int gridSizeX = 6;
   
    private float squareSize = 1f;
   [SerializeField] public GameObject squarePrefab;
   [SerializeField] public float xOffset,yOffset;


    private GameObject[][] slots;

    void Start()
    {
        GenerateGrid();
        
    }

    void GenerateGrid()
    {
        slots = new GameObject[gridSizeX][];

        for (int x = 0; x < gridSizeX; x++)
        {
            slots[x] = new GameObject[gridSizeX];

            for (int y = 0; y < gridSizeX; y++)
            {
                Vector2 spawnPosition = new Vector2(x * squareSize, y * squareSize) + new Vector2(xOffset, yOffset);
                slots[x][y] = Instantiate(squarePrefab, spawnPosition, Quaternion.identity, transform);
            }
        }
    }

   public GameObject slotAt(int x, int y)
    {
        return slots[x][y];
    }

    public int OccupiedCount()
    {
        int count = 0;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeX; y++)
            {
                if (slots[x][y].GetComponent<SlotStatus>().isOccupying() == true) { count++; }
            }
        }

        return count;
    }

    IEnumerator LoadLevel(int levelIndex)
    {

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }

    private void Update()
    {
        if(OccupiedCount() == 36)
        {
            LoadLevel(4);
        }
    }
}