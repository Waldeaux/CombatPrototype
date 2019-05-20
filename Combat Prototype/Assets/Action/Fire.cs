using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Action
{
	public override string actionName
	{
		get { return "Fire"; }
	}
	public static Fire instance { get; set; }
	public override void DoAction(Character actor, List<GameObject> targets)
	{

		actor.DealDamage(1, targets);
	}
}
