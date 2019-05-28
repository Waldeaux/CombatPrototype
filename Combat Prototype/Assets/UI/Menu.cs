using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
	public List<MenuOption> options;
	private int currentIndex;
	private int currentPage;
	int pageCount = 0;
	public void CreateMenu(List<MenuOption> optionsInput)
	{
		options = new List<MenuOption>();
		//options = optionsInput;
		foreach(MenuOption option in optionsInput)
		{
			options.Add(option);
			print(option.anchorY);
		}
		int count = options.Count;
		float optionsize = .25f;// optionsInput[0].anchorY;
		//Decide on menu sizes
		if (count*optionsize >= 1)
		{
			//Set menu to max size
			pageCount = (int)((1 / optionsize)/1);
			//max size = pageCount*optionsize;
		}
		else {
			//Set menu to count*option size
		}
		currentPage = 0;
		RenderPage();
	}
	public void HandleInput()
	{
		options[currentIndex].Highlight(false);
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			currentIndex--;
			if(currentIndex < 0)
			{
				currentIndex = options.Count - 1;
			}
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			currentIndex++;
			if (currentIndex > options.Count - 1)
			{
				currentIndex = 0;
			}
		}
		if (currentIndex / 5 != currentPage)
		{
			currentPage = currentIndex / 5;
		}
		options[currentIndex].Highlight(true);
		if (Input.GetKeyDown(KeyCode.Return))
		{
			options[currentIndex].Activate();
		}
	}

	public void RenderPage()
	{
		print("rendering");
		for (int x = currentPage; x < currentPage + 5 && x < options.Count; x++)
		{
			print(options[x].anchorY);
			RectTransform dummyRt = options[x].gameObject.GetComponent<RectTransform>();
			options[x].transform.SetParent(transform);
			dummyRt.anchorMax = new Vector2(.95f, 1-(x)*options[x].anchorY);
			dummyRt.anchorMin = new Vector2(0.05f, 1 - (x + 1) * options[x].anchorY);
			dummyRt.offsetMin = Vector2.zero;
			dummyRt.offsetMax = Vector2.zero;
		}
	}
}
