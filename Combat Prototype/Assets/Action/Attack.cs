using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack :Action
{
    public override string actionName { get; }
    public List<GameObject> targets;
    public Character actor;
    public Transform actorTransform;
    float timer;
    public override void DoAction(Character actorInput, List<GameObject> targetsInput)
    {
        actor = actorInput;
        actorTransform = actorInput.gameObject.transform;
        targets = targetsInput;
        actor.actionDelegate += MoveCharacter;
        actor.currentState = Character.State.ability;
        GameController.Instance.combatController.SwitchToTurn();
    }
    
    public void MoveCharacter()
    {
        Vector3 targetPoint = targets[0].transform.position;
        //targetPoint.y = actorTransform.position.y;
        Vector3 moveVector = (targetPoint - actorTransform.position);
        float mag = moveVector.magnitude;
        Vector3 distanceVector = moveVector.normalized * (mag - 1.25f);
        mag = distanceVector.magnitude;
        if(mag > (moveVector.normalized*Time.deltaTime*10).magnitude)
        {
            moveVector = (moveVector.normalized * Time.deltaTime * 10);
        }
        else
        {
            moveVector = distanceVector;
        }
        actorTransform.position += moveVector;
        targetPoint = targets[0].transform.position;
        moveVector = targetPoint - actorTransform.position;
        mag = moveVector.magnitude - 1.26f;
        print(mag);
        if (mag <= 0)
        {
            actor.actionDelegate -= MoveCharacter;
            DealDamage();
        }
    }

    private void DealDamage()
    {
        actor.DealDamage(5, targets);
        Rigidbody dummyRb;
        foreach(GameObject target in targets)
        {
            dummyRb = target.GetComponent<Rigidbody>();
            if(dummyRb != null)
            {
                dummyRb.AddForce((target.transform.position - actor.transform.position).normalized * 5);
            }
        }
        timer = 0;
        actor.actionDelegate += CheckIfResumeTurns;
        actor.actionDelegate += CheckIfBounce;
    }

    private void CheckIfResumeTurns()
    {
        //print(timer);
        timer += Time.deltaTime;
        if(timer >= 1f)
        {
            GameController.Instance.combatController.SwitchToTurn();
            actor.actionDelegate -= CheckIfResumeTurns;
        }
    }
    private void CheckIfBounce()
    {

        //print(Physics.Raycast(actor.transform.position += new Vector3(0, -.4f), new Vector3(0, -1, 0), .11f));
        if (Physics.Raycast(actor.transform.position + new Vector3(0, -.4f), new Vector3(0, -1, 0), .11f))
        {
            Rigidbody rb = actor.gameObject.GetComponent<Rigidbody>();
            int signMultiplier = -Math.ReturnSign((targets[0].transform.position - actor.transform.position).x);
            print(signMultiplier);
            if(rb != null)
            {
                rb.AddForce(new Vector3(signMultiplier * 5, 5).normalized * 5, ForceMode.Impulse);
            }
            actor.actionDelegate -= CheckIfBounce;
        }
        
    }
    private void End()
    {

        print("Done");
        actor.currentState = Character.State.none;
    }
}
