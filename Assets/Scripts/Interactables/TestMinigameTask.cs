using UnityEngine;

public class TestMinigameTask : Interactable
{
    [SerializeField] private GameObject minigamePrefab;

    private void Start()
    {
        MinigameManager.Instance.StartMinigame(minigamePrefab, OnMinigameFinished);
    }

    public override void Interact(PlayerInteraction player)
    {
        MinigameManager.Instance.StartMinigame(minigamePrefab, OnMinigameFinished);
    }

    private void OnMinigameFinished(bool success)
    {
        if (success)
        {
            Debug.Log("succes");
            Destroy(gameObject);
        }

        else
        {
            Debug.Log("failed");
        }
    }
}