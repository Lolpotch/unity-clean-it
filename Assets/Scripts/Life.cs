using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] GameObject losePanel = null;

    [SerializeField] GameObject[] lifes = null;
    [SerializeField] Sprite lifeFull = null;
    [SerializeField] Sprite lifeBlank= null;


    int life;
    int maxLife = 3;

    AudioManager audioManager;
    Sound stageMusic;

    void Awake()
    {
        life = maxLife;
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        Displaylife(life);
        stageMusic = audioManager.GetClip("Music Stage");
        stageMusic.Play();
    }

    void Displaylife(int life)
    {
        for(int i = 0; i < maxLife; i++)
        {
            if(i < life)
            {
                lifes[i].GetComponent<Image>().sprite = lifeFull;
            }else
            {
                lifes[i].GetComponent<Image>().sprite = lifeBlank;
            }
        }
    }

    public void Heal()
    {
        life = maxLife;
        Displaylife(life);
    }

    public void AddLife(int change)
    {
        life += change;

        if(life <= 0)
        {
            life = 0;
            YouLose();
        }

        Displaylife(life);
    }

    void YouLose()
    {
        stageMusic.Stop();
        TypewriterEffect.isWin = false;
        losePanel.SetActive(true);
    }
}
