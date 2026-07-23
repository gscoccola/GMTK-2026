using UnityEngine;

public class NPC : Interactable
{
    public override void Interact(PlayerInteraction player)
    {
        Debug.Log("NPC");
        //Add whatever u want for the interact event here
    }
}