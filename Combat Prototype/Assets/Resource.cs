using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Resource
{
	public string resourceName;
	public int max;
	public int current;
	public ResourceDisplay resourceDisplay;
	
	public delegate void CalculateResourceGainModifier(ref float modifer);
	public event CalculateResourceGainModifier thisResourceGain;
	public void Initialize(ResourceDisplay displayInput, string nameInput, int maxInput)
	{
		resourceDisplay = displayInput;
		resourceDisplay.Initialize(this);
		resourceName = nameInput;
		SetMax(maxInput);
		RestoreToFull();
	}

	public bool AtMax()
	{
		return current == max;
	}
	public void RestoreToFull()
	{
		SetCurrent(max);
	}
	public void SetMax(int maxInput)
	{
		max = maxInput;
		if(resourceDisplay != null) { 
		resourceDisplay.updateResourceMax(max.ToString());
		}
	}

	public void AdjustMax(int adjustmentInput)
	{
		max += adjustmentInput;
		if (resourceDisplay != null)
		{
			resourceDisplay.updateResourceMax(max.ToString());
		}
	}

	public void SetCurrent(int currentInput)
	{
		current = currentInput;
		if (resourceDisplay) { 
		resourceDisplay.updateResourceCurrent(current.ToString());
		}
	}

	public float GetGainModifier()
	{

		float modifier = 1;
		if (thisResourceGain != null)
		{
			thisResourceGain.Invoke(ref modifier);
		}
		return modifier;
	}
	public void AdjustCurrent(int adjustmentInput)
	{
		current += adjustmentInput;
		if(current > max)
		{
			current = max;
		}
		if (resourceDisplay)
		{
			resourceDisplay.updateResourceCurrent(current.ToString());
		}
	}

}
