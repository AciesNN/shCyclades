using UnityEngine;
using System.Collections;

public class Menu_Sh: MonoBehaviour {

	static public Menu_OutManager Out {
		get { return Menu_OutManager.GetInstance(); }
	}
	
	static public Menu_InManager In {
		get { return Menu_InManager.GetInstance(); }
	}
	
	static public Menu_GameStateManager GameState {
		get { return Menu_GameStateManager.GetInstance(); }
	}

	static public Menu_ShmiplManager Sрmipl {
		get { return Menu_ShmiplManager.GetInstance(); }
	}

}
