﻿using UnityEngine;

public class DevIntro : MonoBehaviour
{
    public void LoadStartScene()
    {
        FindObjectOfType<GameManager>().LoadScene("Title Screen");
    }
}
