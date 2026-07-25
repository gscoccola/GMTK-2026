using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    public InputActions Actions { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Actions = new InputActions();
        EnableGameplay();
    }

    public void EnableGameplay()
    {
        DisableAll();
        Actions.Player.Enable();
    }

    public void EnableDialogue()
    {
        DisableAll();
        Actions.Dialogue.Enable();
    }

    public void EnableMinigame()
    {
        DisableAll();
        Actions.Minigame.Enable();
    }

    public void EnableUI()
    {
        DisableAll();
        Actions.UI.Enable();
    }

    private void DisableAll()
    {
        Actions.Player.Disable();
        Actions.Dialogue.Disable();
        Actions.Minigame.Disable();
        Actions.UI.Disable();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Actions.Dispose();
    }
}