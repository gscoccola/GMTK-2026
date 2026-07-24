using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    private Minigame currentMinigame;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            DontDestroyOnLoad(this);
        }

    }

    public void StartMinigame(GameObject minigame, System.Action<bool> callback)
    {
        currentMinigame = Instantiate(minigame).GetComponent<Minigame>();
        currentMinigame.OnMinigameFinished += callback;
        currentMinigame.OnMinigameFinished += CloseMinigame;
        currentMinigame.StartMinigame();
    }

    private void CloseMinigame(bool success)
    {
        currentMinigame.OnMinigameFinished -= CloseMinigame;
        currentMinigame = null;
    }
}