using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    string sceneName;

    GameManager manager;
    Animator anim;
    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTransition(string name)
    {
        sceneName = name;
        anim.Play("Transition_Fade in");
    }


    //Animation event
    public void LoadScene()
    {
        print(sceneName);
        manager.LoadScene(sceneName);
    }
}
