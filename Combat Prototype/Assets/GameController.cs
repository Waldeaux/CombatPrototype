using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public static GameController Instance { get; set; }


	public List<Character> partyMembers;
	
	public GameObject menuCanvas;

	//Prefabs
	public GameObject characterStatusPrefab;
	public GameObject actionOptionPrefab;

	
	public enum GameState { Combat, Overworld}
	public GameState currentState = GameState.Combat;

	public CombatController combatController;
	public MenuController menuController;
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		menuController = GetComponent<MenuController>();
		combatController = GetComponent<CombatController>();
		
		combatController.InitializeCombatController();
	}

	void Update()
	{
        if (menuController.IsMenuActive()) { 
		menuController.HandleInput();
        }
        else { 
        switch (currentState)
		{
			case (GameState.Overworld):
				break;
			case (GameState.Combat):
				combatController.CombatUpdate();
				break;
		}
        }
    }
}
