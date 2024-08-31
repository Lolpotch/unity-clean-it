using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Sound menuSound;
    public static bool gameFinished = false;

    void Awake()
    {
        gameFinished = false;
        menuSound = FindObjectOfType<AudioManager>().GetClip("Menu Sound");
    }

    private void Start()
    {
        Resume();
    }

    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            ReloadScene();
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlayMenuSound()
    {
        menuSound.Play();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
