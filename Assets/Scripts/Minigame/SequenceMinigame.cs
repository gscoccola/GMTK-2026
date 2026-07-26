using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class SequenceMinigame : Minigame
{
    public enum ArrowDirection { Up, Down, Left, Right }

    [Header("UI References")]
    [SerializeField] private Transform promptPanelContainer;
    [SerializeField] private GameObject arrowUIPrefab;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Game Settings")]
    [SerializeField] private int startingSequenceLength = 3;
    [SerializeField] private float displayTimePerItem = 1.0f;
    [SerializeField] private float minDisplayTime = 0.3f;
    [SerializeField] private float timeDecrementPerLevel = 0.1f;
    [SerializeField] private int maxGameLevel = 5;

    private List<ArrowDirection> currentSequence = new List<ArrowDirection>();
    private int playerInputIndex = 0;
    private int currentLevel = 1;
    private bool isPlayingSequence = false;
    private bool isPlayerTurn = false;
    private bool gameIsOver = false;

    //test
    private void Start()
    {
        StartMinigame();
    }

    public override void StartMinigame()
    {
        base.StartMinigame();
        StartNewRound();
    }

    void Update()
    {
        if (!isPlayerTurn || gameIsOver) return;

        ArrowDirection? inputDir = GetInput();
        if (inputDir.HasValue)
        {
            ProcessPlayerInput(inputDir.Value);
        }
    }

    ArrowDirection? GetInput()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame) return ArrowDirection.Up;
            if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame) return ArrowDirection.Down;
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame) return ArrowDirection.Left;
            if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame) return ArrowDirection.Right;
        }

        return null;
    }

    private void StartNewRound()
    {
        base.StartMinigame();
        playerInputIndex = 0;
        isPlayerTurn = false;

         levelText.text = $"Level: {currentLevel}";

        int sequenceLength = startingSequenceLength + (currentLevel - 1);
        GenerateSequence(sequenceLength);

        StartCoroutine(PlaySequenceRoutine());
    }

    void GenerateSequence(int length)
    {
        currentSequence.Clear();
        for (int i = 0; i < length; i++)
        {
            currentSequence.Add((ArrowDirection)Random.Range(0, 4));
        }
    }

    IEnumerator PlaySequenceRoutine()
    {
        isPlayingSequence = true;
        feedbackText.text = "Watch the sequence...";
        ClearPromptUI();
        float currentDisplayTime = Mathf.Max(minDisplayTime, displayTimePerItem - ((currentLevel - 1) * timeDecrementPerLevel));
        foreach (var direction in currentSequence)
        {
            GameObject indicator = Instantiate(arrowUIPrefab, promptPanelContainer);
            SetupArrowUI(indicator, direction);

            yield return new WaitForSeconds(currentDisplayTime);

            Destroy(indicator);
            yield return new WaitForSeconds(0.2f);
        }
        isPlayingSequence = false;
        isPlayerTurn = true;
         feedbackText.text = "Your Turn!";
    }

    void ProcessPlayerInput(ArrowDirection inputDir)
    {
        if (currentSequence[playerInputIndex] == inputDir)
        {
            playerInputIndex++;

            if (playerInputIndex >= currentSequence.Count)
            {
                isPlayerTurn = false;
                if (currentLevel >= maxGameLevel)
                {
                    feedbackText.text = "VICTORY! You beat the whole game!";
                    Finish(true);
                    gameIsOver = true;
                }
                else
                {
                    feedbackText.text = "Correct! Level Up!";
                    Finish(false);
                    currentLevel++;
                    Invoke(nameof(StartNewRound), 1.5f);
                }
            }
        }
        else
        {
            isPlayerTurn = false;
            gameIsOver = true;
            feedbackText.text = "Miss! Game Over.";

            Finish(false); 
        }
    }

    void SetupArrowUI(GameObject container, ArrowDirection dir)
    {
        float angle = 0f;
        switch (dir)
        {
            case ArrowDirection.Up: angle = 0f; break;
            case ArrowDirection.Right: angle = -90f; break;
            case ArrowDirection.Down: angle = 180f; break;
            case ArrowDirection.Left: angle = 90f; break;
        }
        container.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void ClearPromptUI()
    {
        foreach (Transform child in promptPanelContainer)
        {
            Destroy(child.gameObject);
        }
    }
}