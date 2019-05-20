using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
	public Text resourceTitle;
	public Text resourceCurrent;
	public Text resourceMax;

	public void Initialize(Resource resourceInput)
	{
		updateResourceTitle(resourceInput.resourceName);
		updateResourceCurrent(resourceInput.current.ToString());
		updateResourceMax(resourceInput.max.ToString());
	}
	public void updateResourceTitle(string input)
	{
		resourceTitle.text = input;
	}

	public void updateResourceCurrent(string input)
	{
		resourceCurrent.text = input;
	}

	public void updateResourceMax(string input)
	{
		resourceMax.text = input;
	}
}
