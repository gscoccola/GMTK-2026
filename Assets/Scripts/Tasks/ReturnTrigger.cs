using UnityEngine;

public class ReturnTrigger : MonoBehaviour
{
    private FetchTask fetchTask;

    public void Initialize(FetchTask task)
    {
        fetchTask = task;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        fetchTask.CompleteTask();

        Destroy(gameObject);
    }
}