using UnityEngine;
using System.Collections;

public class UIUserInfoPanel: UIGamePanel
{
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
			GodSprite.spriteName = UIConsts.godBigSprites[godName];
		}
	}
	#endregion

	#region Events
	public void OnPressOKButton() {
		UIGamePanel.CloseActivePanel("OnBetPanelClose", ModelPanelCloseResult.OK); //TODO надо избавляться от CloseActivePanel вместо этого надо применять применять параметры метода Hide
	}
	
	public void OnPressCancelButton() {
		UIGamePanel.CloseActivePanel("OnBetPanelClose", ModelPanelCloseResult.CANCEL); //TODO надо избавляться от CloseActivePanel вместо этого надо применять применять параметры метода Hide
	}

	public void IncressBet() {
		++CurrentBet;
	}

	public void DiscressBet() {
		--CurrentBet;
	}
	#endregion
}
