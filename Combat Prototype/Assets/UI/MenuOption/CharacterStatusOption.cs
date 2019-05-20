using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusOption : OptionSubClass
{
	public Text nameDisplay;
	public Text maxHPDisplay;
	public Text maxMPDisplay;
	public Text currentHPDisplay;
	public Text currentMPDisplay;
	
	public override void Activate()
	{
	}

	public void SetFields(CharacterStatus input)
	{
		nameDisplay.text = input.name;
		currentHPDisplay.text = input.currentHealth.ToString();
		currentMPDisplay.text = input.currentMana.ToString();
		print(input.currentHealth);
		print(input.currentMana);
		print(input.maxHealth);
		print(input.maxMana);
		maxHPDisplay.text = input.maxHealth.ToString();
		maxMPDisplay.text = input.maxMana.ToString();
	}
}
