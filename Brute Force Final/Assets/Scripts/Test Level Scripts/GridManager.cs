using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GridManager : MonoBehaviour
{
    private int gridSizeX = 6;
    private float squareSize = 1f;
    private float Offset;
    
    [SerializeField] public GameObject squarePrefab;


    private GameObject[][] slots;

    void Start()
    {
    Offset = gridSizeX * -1f/2 + 0.5f;
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
                Vector2 spawnPosition = new Vector2(x * squareSize, y * squareSize) + new Vector2(Offset, Offset);
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

    IEnumerator LoadLevel(int levelID)
    {
        Debug.Log("Loaded");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelID);

    }

    int counter = 0;
    private void Update()
    {   
        if(counter % 500 == 0)
            // Debug.Log(OccupiedCount());
        counter++;
        if(OccupiedCount() >= 36)
        {
            StartCoroutine(LoadLevel(4));
            
        }
    }
}