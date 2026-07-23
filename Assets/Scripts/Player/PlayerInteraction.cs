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
    public void Interact()
    {

        currentInteractable.Interact(this);

    }
}