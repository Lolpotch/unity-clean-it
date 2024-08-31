using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject finishPanel = null;

    public void TakeReport()
    {
        FindObjectOfType<AudioManager>().GetClip("Music Stage").Stop();
        TypewriterEffect.isWin = true;
        finishPanel.SetActive(true);
    }
}
