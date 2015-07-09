using UnityEngine;
using System.Collections;

// Source: http://answers.unity3d.com/questions/408518/dontdestroyonload-duplicate-object-in-a-singleton.html

public class Singleton<Instance> : MonoBehaviour where Instance : Singleton<Instance> {
	public static Instance _Instance;
	public bool isPersistant;
	
	public virtual void Awake() {
		if(isPersistant) {
			if(!_Instance) {
				_Instance = this as Instance;
			}
			else {
				DestroyObject(gameObject);
			}
			DontDestroyOnLoad(gameObject);
		}
		else {
			_Instance = this as Instance;
		}
	}
}
