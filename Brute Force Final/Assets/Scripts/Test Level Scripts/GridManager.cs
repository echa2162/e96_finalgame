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
    Scene currentScene;
    int sceneBuildIndex;
    int buffer = 0;

    private GameObject[][] slots;

    AudioSource victAudio;

    void Start()
    {
        victAudio = GetComponent<AudioSource>();
    Offset = gridSizeX * -1f/2 + 0.5f;
    GenerateGrid();


        // Get the current active scene
        currentScene = SceneManager.GetActiveScene();

        // Get the build index of the current scene
        sceneBuildIndex = currentScene.buildIndex;

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

        victAudio.Play();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelID);

    }

    int counter = 0;
    private void Update()
    {   
        if(counter % 90 == 0)
        Debug.Log(OccupiedCount());
        counter++;
        if(OccupiedCount() + buffer  == 36)
        {
            buffer = 1;
            StartCoroutine(LoadLevel(sceneBuildIndex+1));
            
        }
    }
}