using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesOption : OptionSubClass
{

	public override void Activate()
	{
		Character currentCharacter = GameController.Instance.combatController.GetCurrentCharacter();
		List<Action> actions = currentCharacter.availableActions;
		foreach(Action action in actions)
		{

		}
	}
}
