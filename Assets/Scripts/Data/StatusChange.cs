using UnityEngine;
using System.Collections;

public enum Status : int {
	SpeedUp = 0,
	DoublePoint,
	Confuse,
	FireBall,
	
	// Must always be the last one
	Powers_Max
}

[System.Serializable]
public class StatusChange
{
	public Status scType;
	public int val1 = 0;
	public float time = 0;
}
