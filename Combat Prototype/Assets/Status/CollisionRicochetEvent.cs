using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRicochetEvent : MonoBehaviour
{
	private GameObject actor;
	private Character actorScript;
	private Vector3 currentVelocity;
	private Rigidbody rb;
	public float timer;
	private float maxTimer;
	// Start is called before the first frame update


	public void Initialize(GameObject actorInput, Vector3 velocity, float timerInput)
	{
		actor = actorInput;
		actorScript = actor.GetComponent<Character>();
		rb = actor.GetComponent<Rigidbody>();
		actorScript.OnCharacterCollision += CharacterCollision;
		actorScript.OnCharacterTurn += HandleStatus;

		rb.useGravity = false;
		rb.velocity = velocity;
		rb.angularVelocity = actor.transform.forward.normalized*velocity.magnitude;
		currentVelocity = velocity;
		maxTimer = timerInput;

		CombatController.switchToActive += StartMoving;
		CombatController.switchToTurn += StopMoving;
	}
	
	private void StartMoving()
	{

		rb.velocity = currentVelocity;
		rb.angularVelocity = actor.transform.forward.normalized * currentVelocity.magnitude;
	}

	private void StopMoving()
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}
	private void HandleStatus(float timeRatio)
	{
		timer += timeRatio * Time.deltaTime;
		//rb.velocity = currentVelocity;
		//Checks if timer is 
		if (timer >= maxTimer)
		{
			
			if ((currentVelocity.normalized * Time.deltaTime *20* timeRatio).magnitude > currentVelocity.magnitude)
			{
				Debug.Log("done");
				RemoveStatus();
			}
			else
			{
				currentVelocity -= currentVelocity.normalized * 20* Time.deltaTime * timeRatio;

				rb.angularVelocity = actor.transform.forward.normalized * currentVelocity.magnitude;
				rb.velocity = currentVelocity;
			}


		}
	}

	private void RemoveStatus()
	{

		currentVelocity = Vector3.zero;
		rb.useGravity = true;
		rb.velocity = currentVelocity;
		actorScript.OnCharacterTurn -= HandleStatus;
		actorScript.OnCharacterCollision -= CharacterCollision;
		CombatController.switchToTurn -= StopMoving;
		CombatController.switchToActive -= StartMoving;
		Destroy(this);
	}
	private void CharacterCollision(Collision collision)
	{
		//actor.GetComponent<Rigidbody>().isKinematic = true;
		Vector3 point = Vector3.zero;
		foreach(ContactPoint contact in collision.contacts)
		{
			point += contact.point;
		}
		point /= collision.contactCount;
		print(Vector3.Project((actor.transform.position - point).normalized, new Vector3(0, 1)));
		print(Vector3.Project((actor.transform.position - point).normalized, new Vector3(0, 1)).magnitude);
		if (Vector3.Project((actor.transform.position - point).normalized, new Vector3(0, 1)).magnitude >= .707f)
		{
			currentVelocity.y = -currentVelocity.y;
		}
		else
		{
			currentVelocity.x = -currentVelocity.x;
		}
		if(collision.gameObject.GetComponent<Character>() != null)
		{
			RemoveStatus();
		}
		rb.velocity = currentVelocity;
		actorScript.TakeDamage(1);
	}
}
