using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float Speed;
	public Text CountText;
	public Text WinText;

	private Rigidbody RBody;
	private int Count;

	void Start ()
	{
		RBody = GetComponent<Rigidbody> ();
		Count = 0;
		SetCountText ();
		WinText.text = "";
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		RBody.AddForce (movement * Speed);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive(false);
			Count++;
			SetCountText();
		}
	}

	void SetCountText()
	{
		CountText.text = "Count: " + Count.ToString();
		if (Count >= 11) {
			WinText.text = "You Win!";
		}
	}
}
