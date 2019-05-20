using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProgrammedAction
{
	//public enum Action { moveTo};
	//public Action thisAction;
	public abstract void doAction(float timeRatio, GameObject actor);
	
}
