using System;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public static Action OnPush;
    private static readonly int IsPush = Animator.StringToHash("isPush");
    private Animator _animator;
    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        OnPush += HandlePush;
    }
    
    private void OnDisable()
    {
        OnPush -= HandlePush;
    }

    private void HandlePush()
    {
        _animator.SetTrigger(IsPush);
    }
    
}
