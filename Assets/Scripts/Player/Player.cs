using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :MonoBehaviour {
    
    [SerializeField] private float MoveSpeed = default;
    [SerializeField] private float RotateSpeed = default;
    [SerializeField] private Animator _Animator = default;

    private Queue<Vector2> WayPoints = default;
    private StateMachine _StateMachine = default;

    private bool _StartMove;
    private bool _StartRotate;

    private void Start()
    {
        _StartMove = false;
        _StartRotate = true;

        CheckClick.Instans.AddPoint += AddWayPoint;
        
        _StateMachine = new StateMachine();

        WayPoints = new Queue<Vector2>();

        MoveState moveState = new MoveState(transform, WayPoints, MoveSpeed, _Animator, this);
        RotateState rotateState = new RotateState(transform, WayPoints, RotateSpeed, this);
        IdleState idleState = new IdleState(_Animator);

        _StateMachine.AddTransition(idleState, rotateState, StartRotate());
        _StateMachine.AddTransition(rotateState, moveState, StartMove());
        _StateMachine.AddAnyTransition(idleState, StopMove());
        _StateMachine.AddAnyTransition(rotateState, StartRotate());
        _StateMachine.SetState(idleState);

        Func<bool> StopMove() => () => WayPoints.Count == 0;
        Func<bool> StartMove() => () => WayPoints.Count > 0 && _StartMove;
        Func<bool> StartRotate() => () => WayPoints.Count > 0 && _StartRotate;

    }

    private void Update()
    {
        _StateMachine.Tick();
    }

    public void AddWayPoint(Vector2 position) => WayPoints.Enqueue(position);

    public void SetMove(bool startMove) => _StartMove = startMove;
    
    public void SetRotate(bool startRotate) => _StartRotate = startRotate;

    public Queue<Vector2> GetPoints() => WayPoints;
}
