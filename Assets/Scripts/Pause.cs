using UnityEngine;

public class Pause : MonoBehaviour
{
    bool paused = false;
    [SerializeField] GameObject pausePanel;
    Sound menuSound;

    void Start()
    {
        menuSound = FindObjectOfType<AudioManager>().GetClip("Menu Sound");
    }

    public void PauseGame()
    {
        paused = true;
        pausePanel.SetActive(true);
        
        Time.timeScale = 0f;

        menuSound.Play();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        
        paused = false;
        pausePanel.SetActive(false);

        menuSound.Play();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!paused)
            {
                PauseGame();
            }else
            {
                ResumeGame();
            }
        }
        
    }
}
