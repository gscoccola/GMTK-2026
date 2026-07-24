using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable currentInteractable;

    private void OnTriggerEnter2D(Collider2D other)
    {
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

    // Press F button
    public void Interact()
    {
        // Add a check here
        currentInteractable.Interact(this);

    }
}