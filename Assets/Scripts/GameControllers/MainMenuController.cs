using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
	public static MainMenuController Instance;

	public GameObject DataKeeperObject;

	// Main
	public GameObject MainPanel;
	public GameObject ContinueButton;

	// Ranking
	public GameObject RankingPanel;
	public GameObject RanksPanel;
	public GameObject RankEntry;

	// Play
	public GameObject PlayPanel;

	// Load
	public GameObject LoadPanel;
	public Transform LoadListPanel;
	public GameObject ProfileEntry;

	// New Game
	public GameObject NewGamePanel;
	
	// Privates
	private int CurRanking = 0;
	private RankingData[] Rankings;

	private void Start()
	{
		if (DataKeeper._Instance == null)
			GameObject.Instantiate (DataKeeperObject);

		IORanking.Init ();
		IOUserProfile.Init ();
		IOUserProfile.GetProfiles ();

		if (!IOUserProfile.LoadedProfile.Equals ("")) {
			ContinueButton.SetActive(true);
		}

		Instance = this;
	}

	/* ****************** *
	 * 		Main
	 * ****************** */
	public void OnContinueClick() {
		if (IOUserProfile.LoadedProfile.Equals ("")) {
			ContinueButton.SetActive(false);
		}

		LoadGame (IOUserProfile.LoadedProfile);
	}

	public void OnPlayClick() {
		this.PlayPanel.SetActive (true);
	}

	public void OnQuitClick() {
		Application.Quit();
	}

	/* ****************** *
	 * 		Play
	 * ****************** */
	public void OnNewGameClick() {
		NewGamePanel.SetActive (true);
	}

	public void OnLoadGameClick() {
		
		// Limpa qualquer child do painel de load
		foreach (Transform load in LoadListPanel) {
			GameObject.Destroy(load.gameObject);
		}
		
		// Recebe a lista de perfis
		string[] profs = IOUserProfile.GetProfiles ();
		
		// Cria uma lista de perfis
		for (int i = 0; i < profs.Length; i++) {
			GameObject loadBtn = Instantiate(this.ProfileEntry) as GameObject;
			MainMenuUIProfiles btnData = loadBtn.GetComponent<MainMenuUIProfiles>();
			btnData.UpdateDisplay(profs[i].Replace("Data/userprof-", "").Replace(".dat", ""));
			loadBtn.transform.SetParent(LoadListPanel);
		}
		
		this.LoadPanel.SetActive (true);
	}

	/* ****************** *
	 * 		New Game
	 * ****************** */
	public void OnNewGameCreateClick(Text input) {
		UserProfile prof = new UserProfile ();
		prof.Name = input.text;

		IOUserProfile.SaveProfile (prof);

		LoadGame ("Data/userprof-" + prof.Name + ".dat");
	}

	public void OnNewGameCancelClick() {
		this.NewGamePanel.SetActive (false);
	}


	/* ****************** *
	 * 		Load
	 * ****************** */
	public void OnProfileLoad(string name)
	{
		LoadGame ("Data/userprof-"+name+".dat");
	}

	public void OnLoadCloseClick()
	{
		LoadPanel.SetActive (false);
	}



	/* ****************** *
	 * 		Ranking
	 * ****************** */
	public void OnRankingClick() {
		this.MainPanel.SetActive (false);
		this.RankingPanel.SetActive (true);
		
		this.Rankings = IORanking.Load ();
	}

	public void OnRankingCloseClick() {
		RankingPanel.SetActive (false);
		MainPanel.SetActive (true);
	}

	public void OnRankingNextClick() {
		CurRanking++;
		if (CurRanking >= (int)IORanking.Ranking.Max) {
			CurRanking = 0;
		}

		UpdateRankDisplay ();
	}

	public void OnRankingPreviousClick() {
		CurRanking--;
		if (CurRanking <= 0) {
			CurRanking = (int)IORanking.Ranking.Max - 1;
		}
		
		UpdateRankDisplay ();
	}

	private void UpdateRankDisplay()
	{

	}

	// =================================
	// 			Internals
	// =================================
	private void LoadGame(string name)
	{
		UserProfile prof = IOUserProfile.LoadProfile (name);

		DataKeeper._Instance.Profile = prof;

		Application.LoadLevel (Constants.LevelSelect);
	}
}
