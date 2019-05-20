using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOption : MonoBehaviour
{
	public string displayText;
	public GameObject highlight;

	public float anchorY;
	public void Highlight(bool showSymbol)
	{
		highlight.SetActive(showSymbol);
	}

	public void Activate()
	{
		GetComponent<OptionSubClass>().Activate();
	}

	public void Render()
	{
		gameObject.GetComponentInChildren<Text>().text = displayText;
	}
	
}
