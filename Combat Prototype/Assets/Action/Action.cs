using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action
{
	public abstract string actionName{ get;}
	public abstract void DoAction(Character actor, List<GameObject> targets);
	public ActionInfo GetActionInfo()
	{
		return new ActionInfo(actionName);
	}
}
