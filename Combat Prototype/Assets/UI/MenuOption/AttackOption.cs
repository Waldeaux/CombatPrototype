using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOption : OptionSubClass
{
    public Attack AttackContainer;
    public delegate void CompleteTargetSelection(List<Character> targets);
    public event CompleteTargetSelection completeTargetSelection;
    public override void Activate()
	{
        GameController.Instance.combatController.SelectTargets(HandleReturn);
        GameController.Instance.menuController.HideMenu();
    }

    public void HandleReturn(List<Character> targets)
    {
        List<GameObject> inputs = new List<GameObject>();
        foreach(Character target in targets)
        {
            inputs.Add(target.gameObject);
        }
        AttackContainer.DoAction(GameController.Instance.combatController.actingCharacter, inputs);
    }
}
