using UnityEngine;
using System.Collections;

public class Sh: MonoBehaviour {

	static public DataManager Data {
		get { return DataManager.GetInstance(); }
	}

	static public OutManager Out {
		get { return OutManager.GetInstance(); }
	}
	
	static public InManager In {
		get { return InManager.GetInstance(); }
	}
	
	static public GameStateManager GameState {
		get { return GameStateManager.GetInstance(); }
	}

	static public SmiplManager Smipl {
		get { return SmiplManager.GetInstance(); }
	}
}
