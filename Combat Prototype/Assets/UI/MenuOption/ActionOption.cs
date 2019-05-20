using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionOption : OptionSubClass
{
	public Text actionName;
	public Action executeAction;
	public override void Activate()
	{
	}

	public void SetFields(ActionInfo input)
	{
		actionName.text = input.name;
	}
}
