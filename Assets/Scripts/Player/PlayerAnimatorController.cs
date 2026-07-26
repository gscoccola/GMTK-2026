using System;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private bool _isRunning;
    private Vector2Int _direction = Vector2Int.down;
    private PlayerMovement _movement;
    private PlayerStats _playerStats;

    private static readonly int HappyDownHash = Animator.StringToHash("PlayerHappyDown");
    private static readonly int HappyUpHash = Animator.StringToHash("PlayerHappyUp");
    private static readonly int HappyLeftHash = Animator.StringToHash("PlayerHappyLeft");
    private static readonly int HappyRightHash = Animator.StringToHash("PlayerHappyRight");

    private static readonly int HappyDownHashRun = Animator.StringToHash("PlayerHappyDownRun");
    private static readonly int HappyUpHashRun = Animator.StringToHash("PlayerHappyUpRun");
    private static readonly int HappyLeftHashRun = Animator.StringToHash("PlayerHappyLeftRun");
    private static readonly int HappyRightHashRun = Animator.StringToHash("PlayerHappyRightRun");

    private static readonly int NeutralDownHash = Animator.StringToHash("PlayerNeutralDown");
    private static readonly int NeutralUpHash = Animator.StringToHash("PlayerNeutralUp");
    private static readonly int NeutralLeftHash = Animator.StringToHash("PlayerNeutralLeft");
    private static readonly int NeutralRightHash = Animator.StringToHash("PlayerNeutralRight");
                                
    private static readonly int NeutralDownHashRun = Animator.StringToHash("PlayerNeutralDownRun");
    private static readonly int NeutralUpHashRun = Animator.StringToHash("PlayerNeutralUpRun");
    private static readonly int NeutralLeftHashRun = Animator.StringToHash("PlayerNeutralLeftRun");
    private static readonly int NeutralRightHashRun = Animator.StringToHash("PlayerNeutralRightRun");

    private static readonly int UpsetDownHash = Animator.StringToHash("PlayerUpsetDown");
    private static readonly int UpsetUpHash = Animator.StringToHash("PlayerUpsetUp");
    private static readonly int UpsetLeftHash = Animator.StringToHash("PlayerUpsetLeft");
    private static readonly int UpsetRightHash = Animator.StringToHash("PlayerUpsetRight");
                                
    private static readonly int UpsetDownHashRun = Animator.StringToHash("PlayerUpsetDownRun");
    private static readonly int UpsetUpHashRun = Animator.StringToHash("PlayerUpsetUpRun");
    private static readonly int UpsetLeftHashRun = Animator.StringToHash("PlayerUpsetLeftRun");
    private static readonly int UpsetRightHashRun = Animator.StringToHash("PlayerUpsetRightRun");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        _playerStats = GetComponent<PlayerStats>();
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
            if (_playerStats.HappynessLevel == HappynessLevel.Happy)
            {
                if (_direction == Vector2Int.down) _animator.Play(HappyDownHashRun);
                else if (_direction == Vector2Int.up) _animator.Play(HappyUpHashRun);
                else if (_direction == Vector2Int.left) _animator.Play(HappyLeftHashRun);
                else if (_direction == Vector2Int.right) _animator.Play(HappyRightHashRun);
            }
            else if (_playerStats.HappynessLevel == HappynessLevel.Neutral)
            {
                if (_direction == Vector2Int.down) _animator.Play(NeutralDownHashRun);
                else if (_direction == Vector2Int.up) _animator.Play(NeutralUpHashRun);
                else if (_direction == Vector2Int.left) _animator.Play(NeutralLeftHashRun);
                else if (_direction == Vector2Int.right) _animator.Play(NeutralRightHashRun);
            }
            else if (_playerStats.HappynessLevel == HappynessLevel.Upset)
            {
                if (_direction == Vector2Int.down) _animator.Play(UpsetDownHashRun);
                else if (_direction == Vector2Int.up) _animator.Play(UpsetUpHashRun);
                else if (_direction == Vector2Int.left) _animator.Play(UpsetLeftHashRun);
                else if (_direction == Vector2Int.right) _animator.Play(UpsetRightHashRun);
            }


        }
        else
        {
            if (_playerStats.HappynessLevel == HappynessLevel.Happy)
            {
                if (_direction == Vector2Int.down) _animator.Play(HappyDownHash);
                else if (_direction == Vector2Int.up) _animator.Play(HappyUpHash);
                else if (_direction == Vector2Int.left) _animator.Play(HappyLeftHash);
                else if (_direction == Vector2Int.right) _animator.Play(HappyRightHash);
            }
            else if (_playerStats.HappynessLevel == HappynessLevel.Neutral)
            {
                if (_direction == Vector2Int.down) _animator.Play(NeutralDownHash);
                else if (_direction == Vector2Int.up) _animator.Play(NeutralUpHash);
                else if (_direction == Vector2Int.left) _animator.Play(NeutralLeftHash);
                else if (_direction == Vector2Int.right) _animator.Play(NeutralRightHash);
            }
            else if (_playerStats.HappynessLevel == HappynessLevel.Upset)
            {
                if (_direction == Vector2Int.down) _animator.Play(UpsetDownHash);
                else if (_direction == Vector2Int.up) _animator.Play(UpsetUpHash);
                else if (_direction == Vector2Int.left) _animator.Play(UpsetLeftHash);
                else if (_direction == Vector2Int.right) _animator.Play(UpsetRightHash);
            }
            
        }
    }
}

