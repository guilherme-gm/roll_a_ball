using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelEntryUI : MonoBehaviour {
	public Text TitleText;
	public Image ThumbImg;
	private int id;

	public void UpdateDisplay(LevelInfo info, int pid) {
		this.id = pid;

		TitleText.text = info.Name;
		ThumbImg.sprite = info.Thumbnail;
	}

	public void OnClick() {
		LevelSelectorController.Instance.OnLevelSelect (this.id);
	}
}
