using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/DamageAbility")]
public class FireObject : ActionObject
{
	public DamageTypes.Types type;
	public override void DoAction(Character actor, List<GameObject> targets)
	{
		actor.DealDamage(1, targets);
	}
}
