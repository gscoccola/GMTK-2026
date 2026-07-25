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
        if (_info != null && _info.Missions != null && _info.Missions.Length > 0)
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
        if (!DialogueManager.Instance.HasLinesLoaded)
        {
            // Daniel: Can be modified to include the npc's name if you want to.
            DialogueManager.Instance.SetDialogue(dialogue);
            stateMachine.ChangeState<DialogueState>();
        }

        else
            DialogueManager.Instance.OnInteract();
    }
}