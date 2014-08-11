using UnityEngine;
using System.Collections;

public class Sh: MonoBehaviour {

	static public DataManager Data {
		get { return DataManager.GetInstance(); }
	}

	static public OutManager Out {
		get { return OutManager.GetInstance(); }
	}
	
	static public GameStateManager GameState {
		get { return GameStateManager.GetInstance(); }
	}
	
	static public MapManager Map {
		get { return MapManager.GetInstance(); }
	}}
