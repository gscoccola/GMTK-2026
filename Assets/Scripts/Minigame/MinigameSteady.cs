using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MinigameSteady : Minigame
{
    // Can be adjusted for difficulty
    [SerializeField] private float bandCenter = 0.5f;
    [SerializeField] private float bandWidth = 0.3f;
    [SerializeField] private float driftSpeed = 0.25f;
    [SerializeField] private float holdSpeed = 0.5f;
    [SerializeField] private float requiredTime = 3f;
    [SerializeField] private float timeLimit = 12f;
    [SerializeField] private float shoveInterval = 2f;
    [SerializeField] private float shoveStrength = 0.15f;

    [SerializeField] private InputActionReference holdAction;
    [SerializeField] private RectTransform bandRect;
    [SerializeField] private RectTransform needleRect;
    [SerializeField] private Image progressFill;
    [SerializeField] private Image bandImage;
    [SerializeField] private Image needleImage;
    [SerializeField] private Color inColor = Color.green;
    [SerializeField] private Color outColor = Color.white;

    private bool escalated;
    private float meter;
    private float inBandTime;
    private float elapsed;
    private float nextShove;
    private int dir;

    public float BandMin => bandCenter - bandWidth * 0.5f;
    public float BandMax => bandCenter + bandWidth * 0.5f;

    public override void StartMinigame()
    {
        base.StartMinigame();
        holdAction.action.Enable();
        meter = bandCenter;
        inBandTime = elapsed = 0f;
        dir = Random.value > 0.5f ? 1 : -1;
        nextShove = shoveInterval;
        UpdateBandUI();
    }

    private void OnDisable()
    {
        holdAction.action.Disable();
    }

    private void Update()
    {
        if (finished)
            return;
        
        elapsed += Time.deltaTime;
        meter += dir * driftSpeed * Time.deltaTime;

        if (!escalated && elapsed > timeLimit / 2)
            ApplyEscalation();

        if (holdAction.action.IsPressed())
            meter -= dir * holdSpeed * Time.deltaTime;

        if (elapsed >= nextShove)
        {
            meter += (Random.value > 0.5f ? 1 : -1) * shoveStrength;
            nextShove = elapsed + shoveInterval * Random.Range(0.7f, 1.3f);
        }

        meter = Mathf.Clamp01(meter);
        bool inBand = meter >= BandMin && meter <= BandMax;

        if (inBand)
            inBandTime += Time.deltaTime;
        
        else
            inBandTime = 0f;

        UpdateUI(inBand);

        if (inBandTime >= requiredTime)
            Finish(true);
        
        else if (elapsed >= timeLimit)
            Finish(false);
    }

    private void UpdateBandUI()
    {
        if (!bandRect)
            return;

        bandRect.anchorMin = new Vector2(0, BandMin);
        bandRect.anchorMax = new Vector2(1, BandMax);
        bandRect.offsetMin = bandRect.offsetMax = Vector2.zero;
    }

    private void UpdateUI(bool inBand)
    {
        if (needleRect)
        {
            needleRect.anchorMin = new Vector2(0.5f, meter);
            needleRect.anchorMax = new Vector2(0.5f, meter);
            needleRect.anchoredPosition = Vector2.zero;
        }

        if (progressFill)
            progressFill.fillAmount = inBandTime / requiredTime;
        
        if (bandImage)
            bandImage.color = inBand ? inColor : outColor * 0.6f;
    }

    public void ApplyEscalation(float bandMul = 0.8f, float driftMul = 1.3f, float shoveMul = 1.5f)
    {
        escalated = true;
        bandWidth *= bandMul;
        driftSpeed *= driftMul;
        shoveStrength *= shoveMul;
        shoveInterval *= 0.8f;
        UpdateBandUI();
    }
}