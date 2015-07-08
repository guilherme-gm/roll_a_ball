using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float Speed;
	public Text CountText;
	public Text WinText;
	public Text SCBuffText;
	public Text SCDebuffText;
	public float SCDisplayTime;

	private int WallsLayer = 8;
	private Rigidbody RBody;
	private int Count;

	private StatusChange[] SC;

	void Start ()
	{
		RBody = GetComponent<Rigidbody> ();
		Count = 0;
		SetCountText ();
		WinText.text = "";

		this.SC = new StatusChange[(int)Status.Powers_Max];
		for (int i = 0; i < (int)Status.Powers_Max; i++) {
			this.SC[i] = new StatusChange();
		}
	}

	void Update ()
	{
		// Updates SC duration
		for (int i = 0; i < (int)Status.Powers_Max; i++) {
			if (this.SC[i].time > 0) {
				this.SC[i].time -= Time.deltaTime;

				// Starts to collide with walls again
				if (this.SC[i].time <= 0 && this.SC[i].scType == Status.FireBall) {
					Physics.IgnoreLayerCollision(0, WallsLayer, false);
				}
			}
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// If it's with Confuse SC
		if (this.SC [(int)Status.Confuse].time > 0) {
			moveHorizontal *= -1;
			moveVertical *= -1;
		}

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		// If it's with SpeedUp SC
		if (this.SC [(int)Status.SpeedUp].time > 0) {
			RBody.AddForce (movement * Speed * this.SC [(int)Status.SpeedUp].val1);
		} else {
			RBody.AddForce (movement * Speed);
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.CompareTag (Constants.Tags.PickUps)) {
			other.gameObject.SetActive(false);

			if (this.SC[(int)Status.DoublePoint].time > 0)
				Count += 1 * this.SC[(int)Status.DoublePoint].val1;
			else
				Count++;

			SetCountText();
		}
		else if (other.gameObject.CompareTag(Constants.Tags.SC)) {
			other.gameObject.SetActive(false);
			SCObject scObj = other.gameObject.GetComponent<SCObject>();
			this.ScStart(scObj.Data);
		}
	}

	private void ScStart(StatusChange sc)
	{
		this.SC [(int)sc.scType] = sc;

		// Starts to ignore walls
		if (sc.scType == Status.FireBall) {
			Physics.IgnoreLayerCollision(0, 8, true);
		}

		DisplayScStart (sc.scType);
	}

	void SetCountText()
	{
		CountText.text = "Count: " + Count.ToString();
		if (Count >= 11) {
			WinText.text = "You Win!";
			IORanking.UpdateRank(Constants.Levels.Level01, GameController.Instance.GameTime, Count);
		}
	}

	void DisplayScStart(Status type)
	{
		string name = "";
		bool isBuff = false;

		switch (type)
		{
		case Status.SpeedUp: name = "SPEED UP!"; isBuff = true; break;
		case Status.DoublePoint: name = "2X!"; isBuff = true; break;
		case Status.Confuse: name = "CONFUSION!"; isBuff = false; break;
		case Status.FireBall: name = "FIREBALL!"; isBuff = false; break;
		}

		StartCoroutine (DisplayScStart(name, isBuff));
	}

	IEnumerator DisplayScStart(string name, bool isBuff)
	{
		if (isBuff) {
			SCBuffText.text = name;
			SCBuffText.gameObject.SetActive (true);

			yield return new WaitForSeconds (SCDisplayTime);

			SCBuffText.gameObject.SetActive (false);
		} else {
			SCDebuffText.text = name;
			SCDebuffText.gameObject.SetActive (true);
			
			yield return new WaitForSeconds (SCDisplayTime);
			
			SCDebuffText.gameObject.SetActive (false);
		}
	}
}
