﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIUsersInfo : UIGamePanel {

	#region ViewWidgets
	public UIGrid Grid;
	public GameObject UserInfoWidgetPrefab;
	public List<UIUserInfoWidget> userInfoWidgets;
	#endregion

	private long players_number { get {return Sh.In.GameContext.GetLong("/players_number");} }

	override public void GameContext_LateInit() {
		userInfoWidgets = new List<UIUserInfoWidget>();

		for (int i = 0; i < players_number; ++i) {
			AddUserInfoWidget("" + i + ". ");
		}

		Grid.Reposition();
	}

	#region UpdateData
	override protected void GameContext_UpdateData_Panel(bool deserialize) {

		List<long> player_order = GetPlayerInformationOrder();
		List<string> player_gods_order = GetPlayerGodsInformationOrder(players_number);

		if (player_order == null)
			return; //TODO это может быть в случае если данных нет (напр игра только началась), по идее это надо обрабатывать как-то более симпатишно

		for (int i = 0; i < players_number; ++i) {
			UIUserInfoWidget w = userInfoWidgets[i];
			long player = player_order[i];

			bool is_current_user = (player == Library.GetCurrentPlayer(Sh.In.GameContext));
			w.SetUser((int)player, is_current_user); 

			Cyclades.Game.Phase phase = Library.GetPhase(Sh.In.GameContext);
			bool alreadyMovedInThisTurn = false;
			if (!is_current_user) {
				if (phase == Phase.AuctionPhase) {
					long bet = Cyclades.Game.Library.Auction_GetCurrentBetForPlayer(Sh.In.GameContext, player);
					if (bet > 0)
						alreadyMovedInThisTurn = true;
				} else if (phase == Phase.TurnPhase) {
					List<long> l = Sh.In.GameContext.GetList<long>("/turn/player_order");
					if (l.IndexOf(player) < 0)
						alreadyMovedInThisTurn = true;
				}
			}
			w.SetAlreadyMovedInThisTurn(alreadyMovedInThisTurn);
		}
	}

	List<long> GetPlayerInformationOrder() {
		Cyclades.Game.Phase phase = Library.GetPhase(Sh.In.GameContext);

		if (phase == Phase.AuctionPhase) {
			return Sh.In.GameContext.GetList<long>("/auction/start_order");
		} else if (phase == Phase.TurnPhase) {
			List<long> res = new List<long>();
			res.AddRange(Sh.In.GameContext.GetList<long>("/auction/player_order"));
			res.Add(Sh.In.GameContext.Get<long>("/turn/current_player"));
			res.AddRange(Sh.In.GameContext.GetList<long>("/turn/player_order"));
			return res;
		} else {
			return null;
		}
	}

	List<string> GetPlayerGodsInformationOrder(long players_number) {
		Cyclades.Game.Phase phase = Library.GetPhase(Sh.In.GameContext);

		if (phase == Phase.AuctionPhase) {
			List<string> res = new List<string>();
			for (int i = 0; i < players_number; ++i) {
				res.Add(Constants.godNone);
			}
			return res;
		} else if (phase == Phase.TurnPhase) {
			if (Sh.In.GameContext.Get<long>("/turn/current_player") == -1)
				return null;
			List<string> res = new List<string>();
			res.Add(GetGodForPlayer(Sh.In.GameContext.Get<long>("/turn/current_player")));
			List<long> players_order = Sh.In.GameContext.GetList<long>("/turn/player_order");
			foreach (long pl in players_order)
				res.Add(GetGodForPlayer(pl));
			for (int i = res.Count; i < players_number; ++i) {//остаток набиваем результат черным цветом - знаком того, что это аукционный игрок
				res.Add(Constants.godNone);
			}
			return res;
		} else {
			return null;
		}
	}

	string GetGodForPlayer(long player) {
		return Cyclades.Game.Library.Auction_GetGodByNumber(
			Sh.In.GameContext,
			Cyclades.Game.Library.Auction_GetCurrentGodBetForPlayer(Sh.In.GameContext, player)
		);
	}
	#endregion

	private void AddUserInfoWidget(string name_prefix) {
		GameObject userInfoWidget_go = (GameObject) NGUITools.AddChild(Grid.gameObject, UserInfoWidgetPrefab);
		userInfoWidget_go.name = name_prefix + userInfoWidget_go.name;
		UIUserInfoWidget userInfoWidget = userInfoWidget_go.GetComponent<UIUserInfoWidget>();
		userInfoWidgets.Add (userInfoWidget);
	}
}
