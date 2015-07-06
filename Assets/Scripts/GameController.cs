using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
	public static GameController Instance;

	public Text GameTimeText;
	public bool IsPaused { get; private set; }

	private float GameTime = 0f;
	private float DefaultTimeScale;

	void Start()
	{
		Instance = this;
		this.IsPaused = false;
		this.DefaultTimeScale = Time.timeScale;

		UpdateTimeDisplay ();
	}

	void Update()
	{
		if (!IsPaused) {
			this.GameTime += Time.deltaTime;
			UpdateTimeDisplay();
		}

		if (Input.GetButtonDown ("Pause")) {
			IsPaused = !IsPaused;
			Debug.Log("Game Pause: " + IsPaused);

			if (this.IsPaused)
			{
				Time.timeScale = 0;
			}
			else
			{
				Time.timeScale = this.DefaultTimeScale;
			}
		}
	}

	void UpdateTimeDisplay()
	{
		GameTimeText.text = (int)GameTime + " s";
	}

}
