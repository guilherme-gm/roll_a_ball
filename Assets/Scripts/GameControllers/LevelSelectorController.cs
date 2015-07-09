using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LevelInfo
{
	public Sprite Thumbnail;
	public Sprite BigScreen;
	public string Name;
	public string Description;
}

public class LevelSelectorController : MonoBehaviour
{
	public static LevelSelectorController Instance;

	// UI References
	public Text PointsText;

	// 
	public LevelInfo[] Levels = new LevelInfo[10];

	// Use this for initialization
	void Start ()
	{
		Instance = this;

		if (DataKeeper._Instance == null) {
			Debug.LogError ("DataKeeper not found.");
			Application.LoadLevel (Constants.MainMenu);
			return;
		}

		PointsText.text = "Pontos: " + DataKeeper._Instance.Profile.Points;
	}

	public void OnShopClick()
	{
		Application.LoadLevel (Constants.Shop);
	}

	public 	void OnMainMenuClick()
	{
		Application.LoadLevel (Constants.MainMenu);
	}

	public void OnLevelSelect()
	{

	}
}
