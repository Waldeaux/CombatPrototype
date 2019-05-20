using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
	private Text text;
	public float timer;
	void Start()
	{
		timer = 1;
		text = GetComponent<Text>();
	}
    void Update()
    {
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			Destroy(gameObject);
		}
		else
		{
			text.material.color = new Color(1, 1, 1, timer);
		}
    }
}
