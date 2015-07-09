using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopController : MonoBehaviour {

	public static ShopController Instance;

	public Text PointsText;

	// Use this for initialization
	void Start () {
		Instance = this;
		
		if (DataKeeper._Instance == null) {
			Debug.LogError ("DataKeeper not found.");
			Application.LoadLevel (Constants.MainMenu);
			return;
		}
		
		PointsText.text = "Pontos: " + DataKeeper._Instance.Profile.Points;
	}

	public void OnBackClick()
	{
		Application.LoadLevel (Constants.LevelSelect);
	}
}
