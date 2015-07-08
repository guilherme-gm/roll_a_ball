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


	private int CurRanking = 0;
	private RankingData[] Rankings;

	private void Start()
	{
		IORanking.Init ();
	}

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

		this.Rankings = IORanking.Load ();
	}

	public void OnQuitClick() {
		Application.Quit();
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
}
