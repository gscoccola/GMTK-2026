using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private string npcName;
    [SerializeField] private string[] dialogue;

    public override void Interact(PlayerInteraction player)
    {
        Debug.Log("NPC");
        Debug.Log("There should probably be some NPC states here");
        DialogueManager.Instance.SetDialogue(dialogue);
        //stateMachine.ChangeState<DialogueState>();
        // Daniel: Should this apply to all actors?
        DialogueManager.Instance.OnInteract();
    }
}