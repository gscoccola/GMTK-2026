using UnityEngine;

public class PlayerStats : MonoBehaviour, IStateSaver
{
    public HappynessLevel HappynessLevel = HappynessLevel.Happy;
    private void Start()
    {
        HappynessLevel = PersistentData.Instance.GetPlayerState().PlayerHappynessLevel;
        PersistentData.Instance.AddSaver(this);
    }

    public void SaveState()
    {
        PersistentData.Instance.SavePlayerState(new PlayerSavedState(HappynessLevel));
    }
}



public enum HappynessLevel
{
    Happy,
    Neutral,
    Upset,
}