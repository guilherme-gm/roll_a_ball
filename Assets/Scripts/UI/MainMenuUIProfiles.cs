using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUIProfiles : MonoBehaviour
{
	public Text ProfileName;

	public void UpdateDisplay(string name) {
		ProfileName.text = name;
	}

	public void OnClickEvent ()
	{
		MainMenuController.Instance.OnProfileLoad (ProfileName.text);
	}
}
