using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRicochet : ProgrammedAction
{
	public float timer;
	public float targetTimer;
	public override void doAction(float timeRatio, GameObject actor)
	{
		timer += Time.deltaTime * timeRatio;
		if(timer >= targetTimer)
		{
			Debug.Log("done");
		}
	}

	public CollisionRicochet(GameObject actorInput)
	{
	}


}
