using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckClick : MonoBehaviour
{

	#region Singltone
	public static CheckClick Instans;

	private void Awake()
	{
		if(Instans != null)
			Destroy(gameObject);
		else
			Instans = this;
	}

	#endregion

	public event Action<Vector2> AddPoint;

	public void Click()
	{
		Vector2 mousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			
		AddPoint.Invoke(mousPosition);
			
		GameObject temp = PoolObjects.Instans.GetFromPool("Point");
		temp.transform.position = mousPosition;
		temp.SetActive(true);

	}

}
