using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable currentInteractable;

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

    public void Interact()
    {
        // Daniel: Should this only be triggered at certain states?
        if (currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }

        // Daniel: Placeholder/test code, remove later
        if (currentInteractable is NPC)
        {
            DialogueManager.Instance.OnInteract();
        }
    }
}