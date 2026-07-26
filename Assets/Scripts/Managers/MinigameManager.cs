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
        Vector3 postion = Camera.main.transform.position;
        currentMinigame = Instantiate(minigame, new Vector3(postion.x, postion.y , 0f), Quaternion.identity).GetComponent<Minigame>();
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