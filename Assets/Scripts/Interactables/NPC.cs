using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private string npcName;
    [SerializeField] private string[] dialogue;
    private bool _isInteracting;

    public override void Interact(PlayerInteraction player)
    {
        if (!DialogueManager.Instance.HasLinesLoaded) DialogueManager.Instance.SetDialogue(dialogue);
        //stateMachine.ChangeState<DialogueState>();
        // Daniel: Should this apply to all actors?
        else DialogueManager.Instance.OnInteract();
    }
}