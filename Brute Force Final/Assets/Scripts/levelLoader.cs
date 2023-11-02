using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    // private Vector3 position;
    // position = new Vector3(439.25f, 107.25f, 0.0f);
    public Animator transition;
    
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (false)
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(levelIndex);
    }
    
}
