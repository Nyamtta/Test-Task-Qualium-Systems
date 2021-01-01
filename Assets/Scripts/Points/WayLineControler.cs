using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayLineControler :MonoBehaviour {
	
	[SerializeField] private LineRenderer _LineRenderer = default;
	
	private Queue<Vector2> WayPoints = default;
	private Transform PlayerPosition = default;


	private void Start()
	{
		WayPoints = new Queue<Vector2>();

		CheckClick.Instans.AddPoint += AddPoint;

		PlayerPosition = FindObjectOfType<Player>().transform;
	}

	private void Update()
	{
		if(_LineRenderer.positionCount > 1)
		{
			Trecing();
		}
	}

	private void Trecing()
	{
		_LineRenderer.SetPosition(0, PlayerPosition.position);

		if(Vector3.Distance(_LineRenderer.GetPosition(0), _LineRenderer.GetPosition(1)) < 0.2f)
		{
			DisplacementPoint();
		}
	}

	private void DisplacementPoint()
	{
		for(int i = 1; i < _LineRenderer.positionCount; i++)
		{
			if(i == _LineRenderer.positionCount - 1)
				_LineRenderer.positionCount--;
			else
				_LineRenderer.SetPosition(i, _LineRenderer.GetPosition(i + 1));
		}
	}

	private void AddPoint(Vector2 point)
	{
		_LineRenderer.positionCount++;
		_LineRenderer.SetPosition(_LineRenderer.positionCount - 1, point);
	}
}
