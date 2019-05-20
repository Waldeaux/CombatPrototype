using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAction :ProgrammedAction
{
	public Vector3 targetPoint;
	public float timer;
	public float targetTimer;
	private GameObject actor;
	public override void doAction(float timeRatio, GameObject actor)
	{
		timer += Time.deltaTime * timeRatio;
		actor.transform.position = Vector3.Lerp(actor.transform.position, targetPoint, timer / targetTimer);
	}

	public MoveToAction(GameObject actorInput)
	{
		actor = actorInput;
	}
}
