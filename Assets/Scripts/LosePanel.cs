using UnityEngine;

public class LosePanel : MonoBehaviour
{
    Transition transition;
    AudioManager manager;
    Sound menuSound;


    private void Awake()
    {
        GameManager.gameFinished = true;
        transition = FindObjectOfType<Transition>();
        manager = FindObjectOfType<AudioManager>();

        menuSound = manager.GetClip("Menu Sound");
    }
    
    public void TryAgain()
    {
        print("The game is reloaded!");
        transition.StartTransition("Main");
        menuSound.Play();
    }

    public void Quit()
    {
        print("Back to main screen!");
        transition.StartTransition("Title Screen");
        menuSound.Play();
    }
}
