using UnityEngine;
using UnityEngine.EventSystems;

public class MovingTarget : MonoBehaviour, IPointerDownHandler
{
    private MinigameTarget minigame;
    private bool shouldBeHit;
    private Vector2 direction;
    private Camera cam;
    private float halfWidth;
    private float halfHeight;

    private const float MIN_SPEED = 0.1f;
    private const float MAX_SPEED = 1f;

    public float Speed { get; set; }
    public bool ShouldBeHit => shouldBeHit;

    public void Initialize(MinigameTarget owner)
    {
        Speed = Random.Range(MIN_SPEED, MAX_SPEED);
        minigame = owner;
        shouldBeHit = Random.value < 0.5f;
        direction = Random.insideUnitCircle.normalized;
        cam = Camera.main;

        var sr = GetComponentInChildren<SpriteRenderer>();
        sr.color = shouldBeHit ? Color.white : Color.black;
        halfWidth = sr.bounds.extents.x;
        halfHeight = sr.bounds.extents.y;

        Vector2 min = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = cam.ViewportToWorldPoint(new Vector2(1, 1));
        float x = Random.Range(min.x + halfWidth, max.x - halfWidth);
        float y = Random.Range(min.y + halfHeight, max.y - halfHeight);
        transform.position = new Vector2(x, y);
    }

    private void Update()
    {
        transform.Translate(Speed * Time.deltaTime * direction);

        Vector2 min = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = cam.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 pos = transform.position;

        // If either of the following checks happen, should the player lose immediately?
        if (pos.x - halfWidth < min.x || pos.x + halfWidth > max.x)
            direction.x *= -1;

        if (pos.y - halfHeight < min.y || pos.y + halfHeight > max.y)
            direction.y *= -1;

        direction.Normalize();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        minigame.HitTarget(this, shouldBeHit, eventData);
    }
}