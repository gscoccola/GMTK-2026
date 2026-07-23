using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : Interactable, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 distance;
    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private void Start()
    {
        mainCamera = Camera.main;
        CalculateScreenBounds();
    }

    public override void Interact(PlayerInteraction player)
    {
        Debug.Log("Interacted with DraggableObject");
    }

    private void CalculateScreenBounds()
    {
        // Bottom-left and top-right corners of the screen in world space
        minBounds = mainCamera.ScreenToWorldPoint(Vector2.zero);
        maxBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        distance = mainCamera.ScreenToWorldPoint(eventData.position) - transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        Vector2 newPosition = pointerPosition - distance;
        newPosition = ClampToScreen(newPosition);
        transform.position = newPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SnapIntoPosition();
    }

    private Vector2 ClampToScreen(Vector2 position)
    {
        CalculateScreenBounds();
        float clampX = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        float clampY = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);
        return new Vector2(clampX, clampY);
    }

    private void SnapIntoPosition()
    {
        Vector2 currentPosition = transform.position;
        Vector2 snappedPosition = new(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y));
        snappedPosition = ClampToScreen(snappedPosition);
        transform.position = snappedPosition;
        Debug.Log($"Snapped to: {snappedPosition}");
    }
}