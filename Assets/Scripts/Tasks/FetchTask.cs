using UnityEngine;

public class FetchTask : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Task task;
    [SerializeField] private GameObject collectible;
    [SerializeField] private GameObject returnTriggerPrefab;

    [Header("Settings")]
    [SerializeField] private float timeLimit = 20f;

    //Test
    private void Start()
    {
        task.Activate();
    }

    private bool active;
    private bool collected;
    private float timer;

    private GameObject spawnedReturnTrigger;

    private void Awake()
    {
        if (task == null)
            task = GetComponent<Task>();
    }

    private void OnEnable()
    {
        task.OnActivate.AddListener(BeginTask);
    }

    private void OnDisable()
    {
        task.OnActivate.RemoveListener(BeginTask);
    }

    private void Update()
    {
        if (!active)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            FailTask();
        }
    }

    private void BeginTask()
    {
        active = true;
        collected = false;
        timer = timeLimit;
        collectible.SetActive(true);

        if (spawnedReturnTrigger != null)
            Destroy(spawnedReturnTrigger);
    }

    public void CollectItem()
    {
        if (!active || collected)
            return;

        collected = true;

        if (collectible != null)
            collectible.SetActive(false);

        if (returnTriggerPrefab != null)
        {
            spawnedReturnTrigger = Instantiate(
                returnTriggerPrefab,
                returnTriggerPrefab.transform.position,
                returnTriggerPrefab.transform.rotation);

            ReturnTrigger trigger =
                spawnedReturnTrigger.GetComponent<ReturnTrigger>();

            trigger.Initialize(this);
        }
    }

    public void CompleteTask()
    {
        if (!active)
            return;

        active = false;

        if (spawnedReturnTrigger != null)
            Destroy(spawnedReturnTrigger);

        Debug.Log("Finish task");
        task.OnComplete();
    }

    private void FailTask()
    {
        active = false;

        if (collectible != null)
            collectible.SetActive(false);

        if (spawnedReturnTrigger != null)
            Destroy(spawnedReturnTrigger);

        task.OnFailed();
    }
}