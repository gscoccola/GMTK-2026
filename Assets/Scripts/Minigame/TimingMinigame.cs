using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimingMinigame : Minigame
{
    [Header("UI")]
    [SerializeField] private RectTransform marker;
    [SerializeField] private RectTransform target;
    [SerializeField] private RectTransform bar;
    [SerializeField] private TextMeshProUGUI progressText;

    [Header("Gameplay")]
    [SerializeField] private float markerSpeed = 300f;
    [SerializeField] private float targetWidth = 150f;
    [SerializeField] private float shrinkAmount = 25f;
    [SerializeField] private float speedIncrease = 75f;
    private float markerPosition;
    private int direction = 1;
    private int stage = 0;
    private int currentHits = 0;
    [SerializeField] private int[] requiredHits = { 1, 2, 3 };
    private float LeftLimit => -bar.rect.width / 2f;
    private float RightLimit => bar.rect.width / 2f;

    public override void StartMinigame()
    {
        base.StartMinigame();
        stage = 0;
        currentHits = 0;
        markerPosition = LeftLimit;
        direction = 1;
        markerSpeed = 300f;
        targetWidth = 150f;
        target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
        RandomizeTarget();
        UpdateText();
    }

    void Update()
    {
        markerPosition += direction * markerSpeed * Time.deltaTime;

        if (markerPosition > RightLimit)
        {
            markerPosition = RightLimit;
            direction = -1;
        }

        if (markerPosition < LeftLimit)
        {
            markerPosition = LeftLimit;
            direction = 1;
        }
        marker.anchoredPosition = new Vector2(markerPosition, marker.anchoredPosition.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            CheckHit();
        }
    }
    private void CheckHit()
    {
        float targetLeft = target.anchoredPosition.x - target.rect.width / 2f;
        float targetRight = target.anchoredPosition.x + target.rect.width / 2f;
        bool success = markerPosition >= targetLeft && markerPosition <= targetRight;

        if (success)
        {
            currentHits++;
            if (currentHits >= requiredHits[stage])
            {
                stage++;

                if (stage >= requiredHits.Length)
                {
                    Finish(true);
                    return;
                }

                currentHits = 0;
                targetWidth = Mathf.Max(35f, targetWidth - shrinkAmount);
                target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
                markerSpeed += speedIncrease;
                RandomizeTarget();
            }
        }
        else
        {
            currentHits = 0;
        }

        UpdateText();
    }
    void RandomizeTarget()
    {
        float min = LeftLimit + targetWidth / 2f;
        float max = RightLimit - targetWidth / 2f;
        float x = Random.Range(min, max);
        target.anchoredPosition = new Vector2(x, target.anchoredPosition.y);
    }
    void UpdateText()
    {
        progressText.text =$"Stage {stage + 1}/3\n{currentHits}/{requiredHits[stage]}";
    }
}