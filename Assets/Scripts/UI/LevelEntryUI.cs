using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelEntryUI : MonoBehaviour {
	public Text TitleText;
	public Image ThumbImg;
	private LevelInfo LevelInf;

	public void UpdateDisplay(LevelInfo info) {
		this.LevelInf = info;

		TitleText.text = info.Name;
		ThumbImg.sprite = info.Thumbnail;
	}

	public void OnClick() {
		LevelSelectorController.Instance.OnLevelSelect (this.LevelInf);
	}
}
