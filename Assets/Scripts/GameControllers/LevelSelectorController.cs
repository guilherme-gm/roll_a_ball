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
	public string File;
}

public class LevelSelectorController : MonoBehaviour
{
	public static LevelSelectorController Instance;

	// UI References
	public Text PointsText;
	// Level Info
	public Text LevelNameText;
	public Image LevelImage;
	public Text LevelDescText;

	public GameObject PreviewDisplay;
	public Transform LevelList;
	public GameObject LevelEntry;

	// 
	public LevelInfo[] Levels = new LevelInfo[10];
	private int SelectedLevel;

	// Use this for initialization
	void Start ()
	{
		Instance = this;

		if (DataKeeper._Instance == null) {
			Debug.LogError ("DataKeeper not found.");
			Application.LoadLevel (Constants.MainMenu);
			return;
		}

		foreach (Transform child in LevelList) {
			GameObject.Destroy(child);
		}

		for (int i = 0; i < Levels.Length; i++) {
			GameObject lvlBtn = GameObject.Instantiate(LevelEntry) as GameObject;
			LevelEntryUI lvEntry = lvlBtn.GetComponent<LevelEntryUI>();
			lvEntry.UpdateDisplay(Levels[i], i);

			lvlBtn.transform.SetParent(LevelList);
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

	public void OnLevelSelect(int id)
	{
		LevelNameText.text = Levels[id].Name;
		LevelImage.sprite = Levels[id].BigScreen;
		LevelDescText.text = Levels[id].Description;

		this.SelectedLevel = id;

		PreviewDisplay.SetActive (true);
	}

	public void OnPlayClick()
	{
		Application.LoadLevel (Levels [this.SelectedLevel].File);
	}
}
