using UnityEngine;

public class PlayerStats : MonoBehaviour, IStateSaver
{
    private HappynessLevel _happynessLevel = HappynessLevel.Neutral;
    private void Start()
    {
        _happynessLevel = PersistentData.Instance.GetPlayerState().PlayerHappynessLevel;
        PersistentData.Instance.AddSaver(this);
    }

    public void SaveState()
    {
        PersistentData.Instance.SavePlayerState(new PlayerSavedState(_happynessLevel));
    }
}



public enum HappynessLevel
{
    Neutral,
    Happy,
    Upset,
}