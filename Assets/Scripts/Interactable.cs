using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] InteractableName interactableName = new InteractableName();

    public void PlayerInteract()
    {
        switch(interactableName)
        {
            case InteractableName.NPC :
                GivenMask();
                break;

            case InteractableName.Sink :
                HealPlayer();
                break;

            case InteractableName.Finish:
                Finish();
                break;


            default:
                Debug.LogError("You forgot to set the InteractableName, silly!");
                break;
        }
    }

    void GivenMask()
    {
        GetComponent<NPCBehaviour>().GivenMask();
    }

    void HealPlayer()
    {
        GetComponent<Sink>().HealPlayer();
    }

    void Finish()
    {
        print("You finished the game!");
        GetComponent<Finish>().TakeReport();
    }
}

enum InteractableName
{
    None,
    NPC,
    Sink,
    Finish
};
