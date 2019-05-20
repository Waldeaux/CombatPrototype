using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellZerk :MonoBehaviour
{
	public int stacks;
	public int maxStacks;
    // Start is called before the first frame update
    void Start()
    {
		stacks = 1;
		gameObject.GetComponent<Character>().GetResource("AP").thisResourceGain += CalcResource;
    }
	
	void CalcResource(ref float input)
	{
		input *= 1+stacks * .05f;
	}

	public void AdjustStacks(int input)
	{
		stacks += input;
		if(stacks >= maxStacks)
		{
			stacks = maxStacks;
		}
	}
}
