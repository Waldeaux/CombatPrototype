using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCondition : MonoBehaviour
{
	public Vector3 launchVector;
	public float length;
	private Character thisCharacter;
    // Start is called before the first frame update
    void Start()
    {
		thisCharacter = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.P))
		{
			CollisionRicochetEvent collision = gameObject.AddComponent<CollisionRicochetEvent>();
			collision.Initialize(this.gameObject, launchVector, length);
		}
    }
}
