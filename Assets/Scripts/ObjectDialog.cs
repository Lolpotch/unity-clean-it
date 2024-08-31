using UnityEngine;

public class ObjectDialog : MonoBehaviour
{
    [SerializeField] GameObject dialog = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            dialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialog.SetActive(false);
        }
    }
}
