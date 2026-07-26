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

    // Daniel: Slight bug here, the state machine changes to the gameplay state after this triggers, so the test mini-game never finishes
    // But otherwise it functions as normal.
    public void StartMinigame(GameObject minigame, System.Action<bool> callback)
    {
        currentMinigame = Instantiate(minigame).GetComponent<Minigame>();
        currentMinigame.OnMinigameFinished += callback;
        currentMinigame.OnMinigameFinished += CloseMinigame;

        // Daniel: Far from ideal but it works for now
        FindAnyObjectByType<MinigameState>().minigame = currentMinigame;
        var sm = FindAnyObjectByType<StateMachine>();
        sm.ChangeState<MinigameState>();

        currentMinigame.StartMinigame();
    }

    private void CloseMinigame(bool success)
    {
        FindAnyObjectByType<StateMachine>().ChangeState<GameplayState>();
        currentMinigame.OnMinigameFinished -= CloseMinigame;
        currentMinigame = null;
    }
}