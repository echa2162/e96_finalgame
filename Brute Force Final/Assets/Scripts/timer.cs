using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class TimerScript : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    private float currentTime;

    public TMP_Text timerText;

    void Start()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }

    void Update()
    {

        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            // Timer has reached zero, you can handle the event here
            Debug.Log("Timer reached zero!");
            StartCoroutine(LoadLevel(11));

        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(currentTime);

        timerText.text = string.Format("{0}", seconds);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelIndex);
    }

}
