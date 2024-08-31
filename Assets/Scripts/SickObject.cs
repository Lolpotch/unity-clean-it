using UnityEngine;

public class SickObject : MonoBehaviour
{
    [SerializeField] GameObject sickSprite = null;
    [SerializeField] GameObject virusParticle = null;

    bool cleaned = false;
    Animator anim;
    Sound cleanSound;
    void Awake()
    {
        anim = GetComponent<Animator>();
        cleanSound = FindObjectOfType<AudioManager>().GetClip("Clean");
    }

    public void OnCleaned()
    {
        if(!cleaned)
        {
            Instantiate(virusParticle, transform.position, Quaternion.identity);
            cleaned = true;

            cleanSound.Play();
            anim.SetBool("IsCleaned", true);
            Destroy(sickSprite);
        }
    }
}