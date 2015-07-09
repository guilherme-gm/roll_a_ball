using UnityEngine;
using System.Collections;

public class DataKeeper : Singleton<DataKeeper>
{
	public UserProfile Profile { get; set; }
}
