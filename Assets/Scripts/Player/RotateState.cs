using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateState :IState {

    private readonly Queue<Vector2> PointToRotate;
    private readonly Transform MyTransfotm;
    private readonly float RotateSpeed;
    private readonly Player _Player;

    private Quaternion RotateAngle;

    public RotateState(Transform myTransform, Queue<Vector2> points, float rotateSpeed, Player player)
    {
        _Player = player;
        PointToRotate = points;
        MyTransfotm = myTransform;
        RotateSpeed = rotateSpeed;
    }

    public void OnEnter()
    {
        Vector2 target = PointToRotate.Peek() - (Vector2)MyTransfotm.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        RotateAngle = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void OnExit()
    {
        return;
    }

    public void Tick()
    {
        MyTransfotm.rotation = Quaternion.Lerp(MyTransfotm.rotation, RotateAngle, RotateSpeed * Time.deltaTime);
        CheckRotate();
    }

	private void CheckRotate()
	{
        if(MyTransfotm.rotation == RotateAngle)
		{
            _Player.SetMove(true);
            _Player.SetRotate(false);
		}
	}
}
