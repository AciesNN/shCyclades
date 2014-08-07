using UnityEngine;
using System.Collections;

public class UIAuctionPanel : UIGamePanel {

	#region ViewWidgets
	public GameObject DiscressBetButton;
	public GameObject InscressBetButton;
	public GameObject OkButton;
	public UILabel CurrenBetLabel;
	public UISprite GodSprite;
	#endregion

	public int MinBet;
	public int MaxBet;

	#region ViewWidgetsSet
	private int currentBet;
	public int CurrentBet {
		get { return currentBet; }
		set { 
			currentBet = value;

			DiscressBetButton.SetActive(currentBet > MinBet);
			InscressBetButton.SetActive(currentBet < MaxBet);
			OkButton.SetActive(currentBet >= MinBet && currentBet <= MaxBet);

			CurrenBetLabel.text = "" + currentBet;
		}
	}
	private string godName;
	public string GodName {
		get { return godName; }
		set {
			godName = value;
			GodSprite.spriteName = UIConsts.godSprites[godName];
		}
	}
	#endregion

	#region Events
	public void OnPressOKButton() {
		UIGamePanel.CloseActivePanel("OnBetPanelClose", ModelPanelCloseResult.OK);
	}
	
	public void OnPressCancelButton() {
		UIGamePanel.CloseActivePanel("OnBetPanelClose", ModelPanelCloseResult.CANCEL);
	}

	public void IncressBet() {
		++CurrentBet;
	}

	public void DiscressBet() {
		--CurrentBet;
	}
	#endregion
}
