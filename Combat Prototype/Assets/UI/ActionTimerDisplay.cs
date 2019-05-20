using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTimerDisplay : MonoBehaviour
{

	public RectTransform loadingBar;

	public void UpdateDisplay(float input)
	{
		loadingBar.anchorMax = new Vector2(input, 1);
		loadingBar.offsetMax = Vector2.zero;
	}
}
