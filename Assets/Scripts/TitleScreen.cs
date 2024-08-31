using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    Transition transition;
    GameManager manager;
    Sound menuSound;

    void Awake()
    {
        transition = FindObjectOfType<Transition>();
        manager = FindObjectOfType<GameManager>();
        menuSound = FindObjectOfType<AudioManager>().GetClip("Menu Sound");
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().GetClip("Music Title").Play();
    }

    public void Play()
    {
        print("Play the game!");

        menuSound.Play();
        transition.StartTransition("Main");
    }

    public void Quit()
    {
        print("Quit the game!");

        menuSound.Play();
        manager.Quit();
    }
}
