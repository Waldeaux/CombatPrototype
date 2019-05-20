using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusOption : OptionSubClass
{
	public override void Activate()
	{
		List<Character> characters = GameController.Instance.partyMembers;
		List<MenuOption> options = new List<MenuOption>();
		GameObject statusPrefab = Object.Instantiate(GameController.Instance.characterStatusPrefab);
		foreach(Character character in characters)
		{
			GameObject dummy = Object.Instantiate(statusPrefab);
			dummy.GetComponent<CharacterStatusOption>().SetFields(character.GetCharacterStatus());
			options.Add(dummy.GetComponent<MenuOption>());
		}
		GameController.Instance.menuController.DisplayOptions(options);
	}
}
