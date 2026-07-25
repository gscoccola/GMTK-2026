using UnityEngine;
using System.Collections.Generic;

public class PersistentData : PersistentSingleton<PersistentData>
{


    private Dictionary<string, NPCSavedState> _npcSavedStates = new Dictionary<string, NPCSavedState>();
    private Dictionary<string, TilemapSavedState> _tilemapSavedStates = new Dictionary<string, TilemapSavedState>();
    private PlayerSavedState _playerSavedState = new PlayerSavedState();
    private GlobalSavedState _globalSavedState = new GlobalSavedState();
    private List<IStateSaver> _activeSavers = new List<IStateSaver>();

    #region Save and load State
    public void SaveGlobalState(GlobalSavedState globalState)
    {
        _globalSavedState = globalState;
    }

    public void SavePlayerState(PlayerSavedState playerState)
    {
        _playerSavedState = playerState;
    }

    public void SaveTilemapState(TilemapSavedState tilemapSavedState, string ID)
    {
        _tilemapSavedStates[ID] = tilemapSavedState;
    }

    public void SaveNPCState(NPCSavedState npcSavedState, string ID)
    {
        _npcSavedStates[ID] = npcSavedState;
    }

    public NPCSavedState GetNPCState(string savedID)
    {
        return _npcSavedStates[savedID];
    }

    public TilemapSavedState GetTilemapState(string savedID)
    {
        return _tilemapSavedStates[savedID];
    }

    public PlayerSavedState GetPlayerState()
    {
        return _playerSavedState;
    }

    public GlobalSavedState GetGlobalState()
    {
        return _globalSavedState;
    }

    #endregion

    public void ClearSavedStates()
    {
        _npcSavedStates.Clear();
        _tilemapSavedStates.Clear();
        _playerSavedState = new PlayerSavedState();
        _globalSavedState = new GlobalSavedState();
    }

    public void AddSaver(IStateSaver saver)
    {
        _activeSavers.Add(saver);
    }

    public void ClearSavers()
    {
        _activeSavers.Clear();
    }

    public void Save() 
    { 
        foreach (IStateSaver saver in _activeSavers)
        {
            saver.SaveState();
        }
    }

}
public class GlobalSavedState
{

}

public class NPCSavedState
{

}

public class PlayerSavedState
{
    public HappynessLevel PlayerHappynessLevel;

    public PlayerSavedState(HappynessLevel happynessLevel = HappynessLevel.Neutral)
    {
        PlayerHappynessLevel = happynessLevel;
    }
}
public class TilemapSavedState
{

}