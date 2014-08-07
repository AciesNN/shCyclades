using UnityEngine;
using System.Collections;

public class UIAdminPanel : UIGamePanel {

	public GameObject MainContext;
	
	public void OnOpenCloseButtonClick() {
		MainContext.SetActive(!MainContext.activeSelf);
	}

}
