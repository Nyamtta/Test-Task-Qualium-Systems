using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

	[SerializeField] private Sprite StartButton = default;
	[SerializeField] private Sprite StopButton = default;

	private float TimeScale = default;

	private void Awake()
	{
		TimeScale = Time.timeScale;
	}

	public void PauseButton(Image image)
	{
		if(Time.timeScale > 0)
		{
			Time.timeScale = 0;
			image.sprite = StartButton;
		}
		else
		{
			Time.timeScale = TimeScale;
			image.sprite = StopButton;
		}
	}

}
