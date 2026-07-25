using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MinigameForce : Minigame
{
    [SerializeField] private Image meterBar;
    [SerializeField] private InputActionReference mashAction;
    [SerializeField] private float timeLimit = 5f;
    [SerializeField] private float pressAmount = 1f;
    [SerializeField] private float startingTarget = 20f;
    [SerializeField] private float startingDrainPerSecond = 2f;
    [SerializeField] private float escalateAtTimeRemaining = 2.5f;
    [SerializeField] private float escalatedTarget = 26f;
    [SerializeField] private float escalatedDrainPerSecond = 4f;

    private float currentMeter;
    private float currentTarget;
    private float currentDrain;
    private float timeRemaining;
    private bool escalated;
    private bool isRunning;

    public override void StartMinigame()
    {
        base.StartMinigame();
        currentMeter = 0f;
        currentTarget = startingTarget;
        currentDrain = startingDrainPerSecond;
        timeRemaining = timeLimit;

        escalated = false;
        isRunning = true;

        if (mashAction != null)
            mashAction.action.Enable();

        UpdateUI();
    }

    private void OnDisable()
    {
        if (mashAction != null)
            mashAction.action.Disable();
    }

    private void Update()
    {
        if (!isRunning)
            return;

        HandleInput();
        HandleDrain();
        HandleTimer();
        HandleEscalation();
        HandleWinCondition();
        UpdateUI();
    }

    private void HandleInput()
    {
        if (mashAction == null)
            return;

        if (mashAction.action.WasPressedThisFrame())
            PressButton();
    }

    private void HandleTimer()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;

            if (currentMeter < currentTarget)
            {
                isRunning = false;
                Finish(false);
            }
        }
    }

    private void HandleDrain()
    {
        currentMeter -= currentDrain * Time.deltaTime;
        currentMeter = Mathf.Max(0f, currentMeter);
    }

    private void HandleEscalation()
    {
        if (escalated)
            return;

        if (timeRemaining <= escalateAtTimeRemaining)
        {
            escalated = true;
            currentDrain = escalatedDrainPerSecond;
            currentTarget = escalatedTarget;
        }
    }

    private void HandleWinCondition()
    {
        if (currentMeter >= currentTarget)
        {
            isRunning = false;
            Finish(true);
        }
    }

    public void PressButton()
    {
        if (!isRunning)
            return;

        currentMeter += pressAmount;
        currentMeter = Mathf.Min(currentMeter, currentTarget);

        if (currentMeter >= currentTarget)
        {
            isRunning = false;
            Finish(true);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (meterBar == null)
            return;

        meterBar.fillAmount = currentMeter / currentTarget;
    }
}