using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    private Vector2 input;
    public Vector2Int RunningDirection;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (input.x > 0) RunningDirection = Vector2Int.right;
        else if (input.x < 0) RunningDirection = Vector2Int.left;
        else if(input.y > 0) RunningDirection = Vector2Int.up;
        else if(input.y < 0) RunningDirection = Vector2Int.down;
        else RunningDirection = Vector2Int.zero;


        rb.linearVelocity = (Vector2)RunningDirection * speed;
    }


    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}
