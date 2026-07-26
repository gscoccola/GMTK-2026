using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float _openDistance = 3.0f;
    private Animator _animator;
    private Transform _playerTransform;
    private bool _isPlayerClose;
    private bool _isOpen;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerTransform = FindAnyObjectByType<PlayerMovement>().transform;
    }

    private void Update()
    {
        _isPlayerClose = (transform.position - _playerTransform.position).magnitude < _openDistance;
        if (_isPlayerClose != _isOpen)
        {
            _isOpen = _isPlayerClose;
            _animator.SetBool("IsPlayerClose", _isOpen);
        }
    }
}
//IsPlayerClose