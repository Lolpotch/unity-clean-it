using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] string[] texts = null;

    [SerializeField] Text display = null;
    [SerializeField] float speed = 0f;
    [SerializeField] float delayEachLine = 0f;
    [SerializeField] float timeBeforeStart = 0f;

    [HideInInspector] public bool isDone = false;

    public static bool isWin;

    void Start()
    {
        StartCoroutine(StartTyping());
    }

    public IEnumerator StartTyping()
    {
        isDone = false;
        string printedLine = "";
        string currentLine = "";

        yield return new WaitForSeconds(timeBeforeStart);

        if(isWin)
        {
            print("You win");
            FindObjectOfType<AudioManager>().GetClip("Music Finish").Play();
        }
        else
        {
            print("You lose!");
            FindObjectOfType<AudioManager>().GetClip("Music Lose").Play();
        }

        for (int i = 0; i < texts.Length; i++)
        {
            currentLine = texts[i];
            for (int j = 0; j < currentLine.Length; j++)
            {
                if(i > 0)
                {
                    display.text = printedLine + "\n" + currentLine.Substring(0, j + 1);
                }else
                {
                    display.text = currentLine.Substring(0, j + 1);
                }
                yield return new WaitForSeconds(1/speed);
            }
            if(i > 0)
            {
                printedLine += "\n" + currentLine;

            }
            else
            {
                printedLine = currentLine;
            }
            yield return new WaitForSeconds(delayEachLine);
        }
        isDone = true;
        anim.SetBool("IsDone", isDone);
    }
}
