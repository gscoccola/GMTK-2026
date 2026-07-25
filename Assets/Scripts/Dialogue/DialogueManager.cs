using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : PersistentSingleton<DialogueManager>
{
    [SerializeField] private GameObject textBox;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float typingSpeed = 0.05f;
    
    private string[] dialogueLines;
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool isTextFullyVisible = false;
    private int visibleCharacterCount = 0;

    public void SetDialogue(string[] newDialogue)
    {
        textBox.SetActive(true);
        dialogueLines = newDialogue;
        currentLineIndex = 0;
        DisplayCurrentLine();
    }

    public void OnSkipPerformed()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            ShowAllCharacters();
            isTyping = false;
            isTextFullyVisible = true;
        }

        else if (isTextFullyVisible)
            NextLine();
    }

    private void DisplayCurrentLine()
    {
        if (currentLineIndex < dialogueLines.Length)
            StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
    }

    private void NextLine()
    {
        currentLineIndex++;
        isTextFullyVisible = false;

        if (currentLineIndex < dialogueLines.Length)
            DisplayCurrentLine();

        else
        {
            dialogueText.text = "";
            textBox.SetActive(false);
            FindAnyObjectByType<StateMachine>().ChangeState<GameplayState>();
        }
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        isTextFullyVisible = false;
        visibleCharacterCount = 0;

        dialogueText.text = text;
        dialogueText.maxVisibleCharacters = 0;

        int totalCharacters = text.Length;

        while (visibleCharacterCount < totalCharacters)
        {
            visibleCharacterCount++;
            dialogueText.maxVisibleCharacters = visibleCharacterCount;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        isTextFullyVisible = true;
    }

    private void ShowAllCharacters()
    {
        dialogueText.maxVisibleCharacters = dialogueText.text.Length;
    }
}