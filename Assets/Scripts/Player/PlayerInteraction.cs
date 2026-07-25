using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField][ReadOnly] private Interactable currentInteractable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Daniel: To avoid running into multiple Interactables at once.
        if (currentInteractable != null)
            return;

        Interactable interactable = other.GetComponent<Interactable>();
        currentInteractable = interactable;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable == currentInteractable)
        {
            currentInteractable = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact(this);
            }
        }

        

        //Dzung: I dont see a need for checking if it is an NPC, you can add the code to the Interact() in NPC.cs
    }
}