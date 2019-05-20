using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
	public delegate void CollisionEvent(Collision collision);
	public event CollisionEvent OnCharacterCollision;

	public delegate void CharacterTurn(float timeRatio);
	public event CharacterTurn OnCharacterTurn;


	public delegate void DealDamageEvent(int actor, List<GameObject> targets);
	public DealDamageEvent dealDamage;


	private Rigidbody rb;
	public GameObject target;
	
	public string characterName;
	public int maxHealth;
	public int maxMana;
	public int maxActions;

	public Resource health = new Resource();
	public Resource mana = new Resource();
	public Resource actions = new Resource();

	public delegate void AutoAbility(Character actor, List<GameObject> targets);
	public AutoAbility autoAbility;

	private ActionTimerDisplay actionTimerDisplay;
	public float actionTimer = 0;

	public float speed;

	public List<Action> availableActions = new List<Action>();
	private enum State { attacking, ability, none}
	private State currentState;
	
	public void InitializeCharacter(CharacterUI input)
	{
		availableActions.Add(Fire.instance);
		rb = GetComponent<Rigidbody>();
		currentState = State.none;
		input.InitializeValues(this);
		health.Initialize(input.healthDisplay, "Health", maxHealth);
		mana.Initialize(input.manaDisplay, "Mana", maxMana);
		actions.Initialize(input.actionDisplay, "AP", maxMana);
		actionTimerDisplay = input.actionTimerDisplay;
		actions.SetCurrent(0);
	}

	public void HandleTurnActive(float timeInput)
	{
		switch (currentState) {
			case (State.ability):

			default:/*
				foreach(Status status in currentStatus)
				{
					status.HandleStatus(timeInput);
				}*/
				if (OnCharacterTurn != null)
				{
					OnCharacterTurn(timeInput);
				}
				if (actions.AtMax()) {
					actionTimer = 0;
				}
				else
				{
					actionTimer += timeInput * speed * Time.deltaTime * actions.GetGainModifier();
					if(actionTimer >= 1)
					{
						HandleAPGain();
					}
				}
				actionTimerDisplay.UpdateDisplay(actionTimer);
				break;
		}
	}

	public void HandleAPGain()
	{
		while (actionTimer >= 1)
		{
			if (autoAbility == null)
			{
				actions.AdjustCurrent(1);
			}
			else
			{
				autoAbility(this, new List<GameObject> {target});
			}
			actionTimer -= 1;

		}
	}
	public void HandleTurnTurn(float timeInput)
	{
		switch (currentState)
		{
			case (State.attacking):

				break;
		}
	}

	public void DealDamage(int damage, List<GameObject> targets)
	{
		foreach(GameObject target in targets)
		{
			if(target.GetComponent<Character>() != null)
			{
				target.GetComponent<Character>().TakeDamage(damage);
			}
		}

		if(GetComponent<SpellZerk>() == null)
		{
			gameObject.AddComponent<SpellZerk>();
		}
		else
		{
			GetComponent<SpellZerk>().AdjustStacks(1);
		}

		dealDamage(damage, targets);
	}
	public void TakeDamage(int damage)
	{
		GameController.Instance.combatController.DamageDisplay(Camera.main.WorldToViewportPoint(transform.position), damage);
		AdjustHealth(-damage);
	}
	public void AdjustHealth(int healthInput)
	{
		health.AdjustCurrent(healthInput);
	}
	void OnCollisionEnter(Collision collision)
	{
		print(collision.gameObject.name);
		if (OnCharacterCollision != null)
		{
			OnCharacterCollision(collision);
		}
	}
	public Resource GetResource(string requestedEvent)
	{

		switch (requestedEvent)
		{
			case ("AP"):
				return actions;
			case ("MP"):
				return mana;
			case ("HP"):
				return health;
		}

		return health;
	}

	public CharacterStatus GetCharacterStatus()
	{
		CharacterStatus returnStatus = new CharacterStatus();
		print(health.max);
		print(health.current);
		print(mana.max);
		print(mana.current);
		returnStatus.name = characterName;
		returnStatus.maxHealth = health.max;
		returnStatus.maxMana = mana.max;
		returnStatus.currentHealth = health.current;
		returnStatus.currentMana = mana.current;
		return returnStatus;
	}

	public List<ActionInfo> GetAvailableActions()
	{
		List<ActionInfo> availableActionsInfo = new List<ActionInfo>();
		foreach(Action action in availableActions)
		{
			availableActionsInfo.Add(action.GetActionInfo());
		}
		return availableActionsInfo;
	}
}
