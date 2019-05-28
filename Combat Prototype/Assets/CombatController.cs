using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{

	public delegate void OnSwitchToTurn();
	public static event OnSwitchToTurn switchToTurn;


	public delegate void OnSwitchToActive();
	public static event OnSwitchToActive switchToActive;

    public delegate void CompleteTargetSelection(List<Character> targets);
    public event CompleteTargetSelection completeTargetSelection;

	public List<Character> totalCombatants = new List<Character>();
	public List<CharacterUI> characterUIs = new List<CharacterUI>();

    public GameObject cursor;

	public int selectedCharacter;
	public GameObject characterDisplay;
	public GameObject canvas;
	public float timeRatio;
	public Character actingCharacter;
	private enum State { selectingCharacter, selectingAction, selectingTarget, active, turn, character}
	private State currentState;

	public Menu currentMenu;

	public GameObject defaultMenu;

	public GameObject damageDisplay;
    
	
    // Start is called before the first frame update
   public void InitializeCombatController()
    {
		SwitchToActive();
		List<GameObject> characterUIObject = new List<GameObject>();
		int x = 0;
        foreach(GameObject character in GameObject.FindGameObjectsWithTag("Combatant"))
		{
			GameObject dummy = Object.Instantiate(characterDisplay);
			dummy.transform.SetParent(canvas.transform);
			RectTransform rt = dummy.GetComponent<RectTransform>();
			rt.anchorMax = new Vector2((x + 1) * .25f, .2f);
			rt.anchorMin = new Vector2(x * .25f, 0);
			rt.offsetMin = Vector2.zero;
			rt.offsetMax = Vector2.zero;
			CharacterUI dummyUI = dummy.GetComponent<CharacterUI>();
			characterUIs.Add(dummyUI);
			
			Character dummyCharacter = character.GetComponent<Character>();
			dummyCharacter.InitializeCharacter(dummyUI);
			totalCombatants.Add(dummyCharacter);
			x++;
			
		}

        cursor.SetActive(false);
    }

	public void CombatUpdate()
	{
        if (Input.GetKeyDown(KeyCode.P))
        {
            print(currentState.ToString());
        }
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SwitchToActive();
		}
		switch (currentState) {
			case (State.active):
				foreach (Character character in totalCombatants)
				{
					character.HandleTurnActive(timeRatio);
				}
				if (Input.GetKeyDown(KeyCode.Space))
				{
					SwitchToTurn();
				}
				break;
			case (State.selectingTarget):
                selectedCharacter = 0;
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    selectedCharacter--;
                    if (selectedCharacter < 0)
                    {
                        selectedCharacter = totalCombatants.Count - 1;
                    }

                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selectedCharacter++;
                    if (selectedCharacter > totalCombatants.Count - 1)
                    {
                        selectedCharacter = 0;
                    }
                }
                cursor.transform.position = totalCombatants[selectedCharacter].gameObject.transform.position + new Vector3(0, 1);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //GetComponent<MenuController>().ShowMenu();
                    //currentMenu = GameController.Instance.menuController.GetMenu();
                    //currentState = State.selectingAction;
                    List<Character> input = new List<Character>();
                    input.Add(totalCombatants[selectedCharacter]);
                    completeTargetSelection(input);
                }
                break;
			case (State.turn):
				if (Input.GetKeyDown(KeyCode.Space))
				{
					SwitchToActive();
				}
				break;
			case (State.selectingCharacter):
				if (Input.GetKeyDown(KeyCode.UpArrow))
				{
					selectedCharacter--;
					if(selectedCharacter < 0)
					{
						selectedCharacter = totalCombatants.Count - 1;
					}

				}
				if (Input.GetKeyDown(KeyCode.DownArrow))
				{
					selectedCharacter++;
					if (selectedCharacter > totalCombatants.Count - 1)
					{
						selectedCharacter = 0;
					}
				}
                cursor.transform.position = totalCombatants[selectedCharacter].gameObject.transform.position + new Vector3(0,1);
				if (Input.GetKeyDown(KeyCode.Return))
				{
                    /*List<ActionInfo> totalInfo= totalCombatants[selectedCharacter].GetAvailableActions();
					GameObject actionOptionExemplar = Object.Instantiate(GameController.Instance.actionOptionPrefab);
					List<MenuOption> optionList = new List<MenuOption>();
					int index = 0;
					foreach(ActionInfo info in totalInfo)
					{
						GameObject dummy = Object.Instantiate(actionOptionExemplar);
						dummy.GetComponent<ActionOption>().SetFields(info);
						optionList.Add(dummy.GetComponent<MenuOption>());
						index++;
					}*/
                    //GameController.Instance.menuController.ShowMenu();
                    //GameController.Instance.menuController.DisplayOptions(optionList);
                    actingCharacter = totalCombatants[selectedCharacter];
                    GetComponent<MenuController>().ShowMenu();
                    currentMenu = GameController.Instance.menuController.GetMenu();
                    currentState = State.selectingAction;
				}
				break;
			case (State.character):
                
				if(actingCharacter == null)
				{
					SwitchToActive();
				}
				else
				{
					actingCharacter.HandleTurnTurn(timeRatio);
				}
				break;
		}
	}
	
    public void SwitchToCharacter()
    {
        print("character");
        currentState = State.character;
        foreach (Character character in totalCombatants)
        {
            character.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
	public void SwitchToTurn()
	{
		foreach (Character character in totalCombatants)
		{
			character.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
		selectedCharacter = 0;
		//GetComponent<MenuController>().ShowMenu();
		currentState = State.selectingCharacter;
        cursor.SetActive(true);
		//currentMenu = GameController.Instance.menuController.GetMenu();
		
		switchToTurn?.Invoke();
	}

	void SwitchToActive()
    {
        print("active");
        foreach (Character character in totalCombatants)
		{
			character.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		}
		currentState = State.active;
		GetComponent<MenuController>().HideMenu();
		switchToActive?.Invoke();
	}

	public void DamageDisplay(Vector3 viewportInput, int damage)
	{
		GameObject dummy = GameObject.Instantiate(damageDisplay);
		Material mat = new Material(dummy.GetComponent<Text>().material);
		dummy.GetComponent<Text>().text = damage.ToString();
		dummy.GetComponent<Text>().material = mat;
		dummy.transform.SetParent(canvas.transform);
		RectTransform rt = dummy.GetComponent<RectTransform>();
		rt.anchorMax = viewportInput + new Vector3(.03f, .02f);
		rt.anchorMin = viewportInput - new Vector3(.03f, .02f);
		rt.offsetMax = Vector3.zero;
		rt.offsetMin = Vector3.zero;
	}

	public Character GetCurrentCharacter()
	{
		return actingCharacter;
	}
    
    public void SelectTargets(CompleteTargetSelection input)
    {
        currentState = State.selectingTarget;
        completeTargetSelection = input;
    }
}
