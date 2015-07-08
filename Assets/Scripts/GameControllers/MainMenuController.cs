using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
	public GameObject MainPanel;
	public GameObject RankingPanel;
	public GameObject RanksPanel;
	public GameObject RankEntry;
	public GameObject LoadPanel;

	public GameObject PlayPanel;

	public void OnContinueClick() {

	}

	public void OnPlayClick() {
		this.PlayPanel.SetActive (true);
	}

	public void OnNewGameClick() {
		Application.LoadLevel ("MiniGame");
	}

	public void OnLoadGameClick() {
		this.LoadPanel.SetActive (true);
	}

	public void OnRankingClick() {
		this.MainPanel.SetActive (false);
		this.RankingPanel.SetActive (true);
	}

	public void OnQuitClick() {
		Application.Quit();
	}

	public void Update()
	{

	}
}
