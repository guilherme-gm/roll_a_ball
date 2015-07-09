using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour {

	public static ShopController Instance;

	// Use this for initialization
	void Start () {
		Instance = this;
	}

	public void OnBackClick()
	{
		Application.LoadLevel (Constants.LevelSelect);
	}
}
