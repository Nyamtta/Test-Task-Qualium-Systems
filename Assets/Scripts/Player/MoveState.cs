using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState :IState {
    
    private readonly Transform _Transform;
    private readonly Queue<Vector2> NextPositions;
    private readonly float MoveSpeed; 
    private readonly Animator _Animator;
    private readonly Player _Player;

    public MoveState(Transform playerTr, Queue<Vector2> nextPositions, float moveSpeed, Animator playerAn, Player player)
    {
        _Player = player;
        _Transform = playerTr;
        NextPositions = nextPositions;
        MoveSpeed = moveSpeed;
        _Animator = playerAn;
    }

    public void OnEnter()
    {
        _Animator.SetTrigger("Move");
    }

    public void OnExit()
    {
        return;
    }

    public void Tick()
    {
        if(NextPositions.Count > 0)
		{
            _Transform.position = Vector2.MoveTowards(_Transform.position, NextPositions.Peek(), MoveSpeed * Time.deltaTime);
            CheckPosition();
		}
    }

	private void CheckPosition()
	{
        if((Vector2)_Transform.position == NextPositions.Peek())
        {
            _Player.SetMove(false);
            _Player.SetRotate(true);
            NextPositions.Dequeue();
        }
	}
}
