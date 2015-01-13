using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {


	void Awake () {
		GameObject.DontDestroyOnLoad(this);
	}
	
}
