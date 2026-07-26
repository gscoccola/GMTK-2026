using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    private Vector2 input;
    public Vector2Int RunningDirection;
    private Rigidbody2D rb;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip[] footstepClips = new AudioClip[2];
    [SerializeField] private float stepInterval = 0.4f;
    private float stepTimer;
    private int footstepIndex = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;
        if (input.x > 0) RunningDirection = Vector2Int.right;
        else if (input.x < 0) RunningDirection = Vector2Int.left;
        else if (input.y > 0) RunningDirection = Vector2Int.up;
        else if (input.y < 0) RunningDirection = Vector2Int.down;
        else RunningDirection = Vector2Int.zero;

        if (RunningDirection == Vector2Int.right || RunningDirection == Vector2Int.left) rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        else if (RunningDirection == Vector2Int.up || RunningDirection == Vector2Int.down) rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

        rb.linearVelocity = (Vector2)RunningDirection * speed;

        if (RunningDirection != Vector2Int.zero)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                AudioClip clipToPlay = footstepClips[footstepIndex];
                if (clipToPlay != null)
                {
                    AudioManager.instance.PlayAudio(clipToPlay);
                }
                footstepIndex = (footstepIndex + 1) % footstepClips.Length;
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}
