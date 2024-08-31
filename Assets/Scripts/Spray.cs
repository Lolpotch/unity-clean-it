using UnityEngine;

public class Spray : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(collision.tag == "Virus")
        {
            Destroy(collision.gameObject);
        }
        */

        if(collision.tag == "Sick Object")
        {
            print("You cleaned it!");
            FindObjectOfType<CounterManager>().AddDisinfected(1);
            collision.GetComponent<SickObject>().OnCleaned();
        }
    }
}
