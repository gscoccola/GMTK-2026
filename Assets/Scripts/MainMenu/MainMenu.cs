using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _setttingsPanel;

    private void Start()
    {
        _startPanel.SetActive(true);
        _setttingsPanel.SetActive(false);
    }

    public void ToggleSetting()
    {
        _setttingsPanel.SetActive(!_setttingsPanel.activeSelf);
        _startPanel.SetActive(!_setttingsPanel.activeSelf);
    }


    public void StartGame()
    {
        LevelLoader.Instance.LoadLevel("Level1");
    }
}
