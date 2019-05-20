using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public GameObject menuCanvas;

	//Prefabs
	public GameObject menuPrefab;
	public GameObject currentMenu;

	

	public GameObject menuObject;
	public Menu menu;
	public List<MenuOption> options;
	public void ShowMenu()
	{
		Menu thisMenu = GetMenu();
		thisMenu.gameObject.SetActive(true);

		/*
		List<Character> characters = GameController.Instance.partyMembers;
		List<MenuOption> options = new List<MenuOption>();
		GameObject statusPrefab = Object.Instantiate(GameController.Instance.characterStatusPrefab);
		foreach (Character character in characters)
		{
			print(character.name);
			GameObject dummy = Object.Instantiate(statusPrefab);
			dummy.GetComponent<CharacterStatusOption>().SetFields(character.GetCharacterStatus());
			options.Add(dummy.GetComponent<MenuOption>());
		}*/

		GameController.Instance.menuController.DisplayOptions(options);
	}

	public void HandleInput()
	{
		if (menu != null)
		{
			menu.HandleInput();
		}
	}
	public Menu GetMenu()
	{
		if(menu == null)
		{
			if(menuObject == null)
			{
				menuObject = Object.Instantiate(menuPrefab);
				menuObject.transform.SetParent(menuCanvas.transform);
				RectTransform rt = menuObject.GetComponent<RectTransform>();
				rt.offsetMax = Vector2.zero;
				rt.offsetMin = Vector2.zero;
			}
			menu = menuObject.GetComponent<Menu>();
		}
		return menu;
	}

	public void HideMenu()
	{
		Destroy(menuObject);
	}
	public void DisplayOptions(List<MenuOption> optionsInput)
	{
		options = optionsInput;
		Menu thisMenu = GetMenu();
		thisMenu.CreateMenu(optionsInput);
	}
}
