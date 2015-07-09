using UnityEngine;
using System.Collections;

public class DataKeeper : MonoBehaviour {

	public static DataKeeper Instance;

	public UserProfile Profile { get; set; }

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);

		Instance = this;
	}


}
