using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private string npcName;
    [SerializeField] private string[] dialogue;

    public override void Interact(PlayerInteraction player)
    {
        // Daniel: Can be modified to include the npc's name if you want to.
        DialogueManager.Instance.SetDialogue(dialogue);
        stateMachine.ChangeState<DialogueState>();
    }
}