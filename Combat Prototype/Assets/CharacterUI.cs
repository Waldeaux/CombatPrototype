using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
	public Text nameDisplay;
	public ResourceDisplay healthDisplay;
	public ResourceDisplay manaDisplay;
	public ResourceDisplay actionDisplay;
	public ActionTimerDisplay actionTimerDisplay;
	private Dictionary<string, GameObject> resourceDict = new Dictionary<string, GameObject>();
    
	public void InitializeValues(Character characterInput)
	{
		nameDisplay.text = characterInput.characterName;
	}
	
	public void AddResourceDisplay(string resourceName, GameObject input)
	{
		resourceDict.Add(resourceName, input);
		int count = resourceDict.Values.Count;
			RectTransform rt = input.GetComponent<RectTransform>();
		rt.anchorMax = new Vector2(1, .625f - .125f * count);
		rt.anchorMin = new Vector2(0, .375f - .125f * count);
		rt.offsetMax = Vector2.zero;
		rt.offsetMin = Vector2.zero;
	}
	
}
