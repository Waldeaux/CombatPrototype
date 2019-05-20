using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{

	public string resourceName;
	int attribute;

	public delegate float CalculateResourceGainModifier();
	public event CalculateResourceGainModifier thisResourceGain;
	public void Initialize(int attributeInput, string nameInput)
	{
		resourceName = nameInput;
		attribute = attributeInput;
	}

}
