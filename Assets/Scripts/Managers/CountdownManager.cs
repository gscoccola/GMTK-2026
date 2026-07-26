using UnityEngine;
using TMPro;

public class CountdownManager : Singleton<CountdownManager>
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject GameOverScreen;
    private float timeLeft;

    [SerializeField] private float InitialTime = 60f;
    public int DifficultyLevel = 1;
    private bool isGameOver;

    private void Start()
    {
        timeLeft = InitialTime;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        timerText.text = (Mathf.RoundToInt(timeLeft)).ToString();

        if (!isGameOver && timeLeft < 0) OnTimeExpired();
    }

    public void IncreaseTimer(float time)
    {
        timeLeft += time;
    }
    public void IncreaseDifficulty()
    {
        DifficultyLevel++;
    }

    private void OnTimeExpired()
    {
        isGameOver = true;
        Time.timeScale = 0;
        
        GameOverScreen.SetActive(true);
    }
}
