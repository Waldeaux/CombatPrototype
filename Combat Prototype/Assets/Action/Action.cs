using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action:MonoBehaviour
{
	public abstract string actionName{ get;}
	public abstract void DoAction(Character actor, List<GameObject> targets);
    public delegate void ActionDelegate();
    public event ActionDelegate actionDelegate;
	public ActionInfo GetActionInfo()
	{
		return new ActionInfo(actionName);
	}
}
