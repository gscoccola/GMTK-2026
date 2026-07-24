using UnityEngine;
using UnityEngine.EventSystems;

public class ConnectNode : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private int pairID;
    [SerializeField] private bool isLeftNode;

    private MinigameConnect minigame;

    public int PairId => pairID;
    public bool IsLeftNode => isLeftNode;
    public bool IsConnected { get; set; }

    public void Initialize(MinigameConnect owner)
    {
        minigame = owner;
        IsConnected = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        minigame?.BeginDrag(this, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        minigame?.ContinueDrag(this, eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        minigame?.EndDrag(this, eventData);
    }
}