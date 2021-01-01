using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState :IState {

    private readonly Animator _Animator;

    public IdleState(Animator playerAnimator)
    {
        _Animator = playerAnimator;
    }
    
    public void OnEnter()
    {
        _Animator.SetTrigger("Idle");
    }

    public void OnExit()
    {
        return;
    }

    public void Tick()
    {
        return;
    }
}
