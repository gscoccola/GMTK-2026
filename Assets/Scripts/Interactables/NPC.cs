using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private string npcName;
    [SerializeField] private string[] dialogue;
    [SerializeField] private NPCInfoSO _info;

    [SerializeField] private NPCMission _currentMission;
    [SerializeField][ReadOnly] private bool _wasBriefed;
    private int _currentMissionIndex;


    private void Start()
    {
        _currentMission = _info.Missions[0];
    }

    public void OnMissionComplete()
    {
        _currentMissionIndex++;
        _currentMission = _info.Missions[_currentMissionIndex];
        _wasBriefed = false;

    }

    public override void Interact(PlayerInteraction player)
    {
        if (!DialogueManager.Instance.HasLinesLoaded) DialogueManager.Instance.SetDialogue(dialogue);
        //stateMachine.ChangeState<DialogueState>();
        // Daniel: Should this apply to all actors?
        else DialogueManager.Instance.OnInteract();
    }
}