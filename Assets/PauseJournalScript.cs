using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseJournalScript : MonoBehaviour
{
    public GameObject journal;
    bool key_is_pressed;
    bool isPause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        journal.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }

    void Pause()
    {
        journal.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }

    public void Resume()
    {
        journal.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        Debug.Log("GAME RESUME");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            key_is_pressed = true;
        }
        else
        {
            key_is_pressed = false;
        }

        if (key_is_pressed && !isPause)
        {
            Pause();
        }
    }

    private void FixedUpdate()
    {

    }


}
