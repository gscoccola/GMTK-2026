using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private FetchTask fetchTask;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        fetchTask.CollectItem();
    }
}