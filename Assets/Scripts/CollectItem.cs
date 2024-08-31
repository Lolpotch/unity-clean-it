using UnityEngine;

public class CollectItem : MonoBehaviour
{
    CounterManager counterManager;

    [SerializeField] GameObject coinParticle = null;
    [SerializeField] GameObject maskParticle = null;

    Sound coinSound, maskSound;

    private void Awake()
    {
        counterManager = FindObjectOfType<CounterManager>();

        AudioManager manager = FindObjectOfType<AudioManager>();
        coinSound = manager.GetClip("Coin");
        maskSound = manager.GetClip("Mask");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Coin":
                Instantiate(coinParticle, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                GetCoin();
                break;

            case "Mask":
                Instantiate(maskParticle, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                GetMask();
                break;


        }
    }

    void GetCoin()
    {
        print("You got a coin!");
        coinSound.Play();
        counterManager.AddCoin(1);
    }

    void GetMask()
    {
        print("You got a mask!");
        maskSound.Play();
        counterManager.AddMask(1);
    }
}
