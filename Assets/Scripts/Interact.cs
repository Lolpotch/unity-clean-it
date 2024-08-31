using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] GameObject interactButton;

    Collider2D interactToWhat;
    bool interact = false;
    void Start()
    {
        interactButton.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && interact)
        {
            Action();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Interactable")
        {
            interact = true;
            interactToWhat = collision;

            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            interact = false;
            interactButton.SetActive(false);
        }
    }

    public void Action()
    {
        interactToWhat.GetComponent<Interactable>().PlayerInteract();
    }

}
