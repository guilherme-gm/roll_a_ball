using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject Player;

	private Vector3 Offset;

	void Start()
	{
		Offset = transform.position - Player.transform.position;
	}

	// Depois de todas as updates
	void LateUpdate () {
		transform.position = Player.transform.position + Offset;
	}
}
