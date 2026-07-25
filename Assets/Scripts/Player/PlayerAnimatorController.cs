using System;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private bool _isRunning;
    private Vector2Int _direction = Vector2Int.down;
    private PlayerMovement _movement;

    private static readonly int HappyDownHash = Animator.StringToHash("PlayerHappyDown");
    private static readonly int HappyUpHash = Animator.StringToHash("PlayerHappyUp");
    private static readonly int HappyLeftHash = Animator.StringToHash("PlayerHappyLeft");
    private static readonly int HappyRightHash = Animator.StringToHash("PlayerHappyRight");

    private static readonly int HappyDownHashRun = Animator.StringToHash("PlayerHappyDownRun");
    private static readonly int HappyUpHashRun = Animator.StringToHash("PlayerHappyUpRun");
    private static readonly int HappyLeftHashRun = Animator.StringToHash("PlayerHappyLeftRun");
    private static readonly int HappyRightHashRun = Animator.StringToHash("PlayerHappyRightRun");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }

    public void SetMovement()
    {
        Vector2Int direction = _movement.RunningDirection;
        if (direction == Vector2Int.zero) _isRunning = false;
        else
        {
            _isRunning = true;
            _direction = direction;
        }
    }

    private void Update()
    {
        SetMovement();
        SetAnimation();
    }

    private void SetAnimation()
    {
        if (_isRunning)
        {
            if (_direction == Vector2Int.down) _animator.Play(HappyDownHashRun);
            else if (_direction == Vector2Int.up) _animator.Play(HappyUpHashRun);
            else if(_direction == Vector2Int.left) _animator.Play(HappyLeftHashRun);
            else if(_direction == Vector2Int.right) _animator.Play(HappyRightHashRun);
        }
        else
        {
            if (_direction == Vector2Int.down) _animator.Play(HappyDownHash);
            else if (_direction == Vector2Int.up) _animator.Play(HappyUpHash);
            else if (_direction == Vector2Int.left) _animator.Play(HappyLeftHash);
            else if (_direction == Vector2Int.right) _animator.Play(HappyRightHash);
        }
    }
}

