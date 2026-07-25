using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCInfo", menuName = "NPCInfo")]

public class NPCInfoSO : ScriptableObject
{
    public NPCMission[] Missions;
}

[Serializable]
public class NPCMission
{
    public string ID;
    public string Description;

    public string[] InitialDialog;
    public string[] RepeatDialog;
}