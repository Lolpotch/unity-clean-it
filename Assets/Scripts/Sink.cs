using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField] GameObject healParticle;
    [SerializeField] GameObject dialogBox;

    Sound healSound;

    bool used = false;

    void Awake()
    {
        healSound = FindObjectOfType<AudioManager>().GetClip("Heal");
    }

    public void HealPlayer()
    {
        if(!used)
        {
            used = true;
            
            FindObjectOfType<Life>().Heal();

            healSound.Play();

            Transform player = FindObjectOfType<PlayerController>().transform;
            Instantiate(healParticle, player.position, Quaternion.identity);
            
            dialogBox.SetActive(false);

            print("You are healed!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !used)
        {
            dialogBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !used)
        {
            dialogBox.SetActive(false);
        }
    }


}
