using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;
using Cyclades.Game.Client;

class MoveUnitEventer: IslandClickMapEventer {
	
	int fromIsland;
	int count;
	int max_units_count;

	#region Events
	override public void Activate() {
		base.Activate();

		fromIsland = -1;

		CalculateAllowedIslandsFrom();
	}

	void OnCountUp() {
		if (count >= max_units_count)
			return;
		count++;
		UIGodPanel.inst.SetAdditionalText("" + count);
	}

	void OnCountDown() {
		if (count <= 1)
			return;
		count--;
		UIGodPanel.inst.SetAdditionalText("" + count);
	}

	public void OnClickCancel() {
		Sh.Out.Send(Messanges.CancelMoveNavy());
		CloseEventer();
	}
	#endregion

	#region Abstract
	override protected void OnClickIsland(int island) {
		HighlightIslands(false);
		if (fromIsland == -1) {
			fromIsland = island;

			max_units_count = Sh.In.GameContext.GetInt("/map/islands/army/[{0}]", island);
			count = max_units_count;

			CalculateAllowedIslandsTo();
			HighlightIslands(true);

			UIInit();
		} else {
			Sh.Out.Send(Messanges.MoveArmy(fromIsland, island, count));
		}
	}
	#endregion

	void CalculateAllowedIslandsFrom() {
		if (Sh.GameState.currentUser != -1) { //todo совершенно лишнее в реальной игре условие
			List<long> islands = Library.Map_GetIslandsByOwner(Sh.In.GameContext, Sh.GameState.currentUser);
			foreach(long island in islands) {
				if(Sh.In.GameContext.GetInt ("/map/islands/army/[{0}]", island) > 0 && Library.Map_GetBridgetIslands(Sh.In.GameContext, island, Sh.GameState.currentUser).Count > 0)
					allowedIslands.Add(island);
			}
		}
	}

	void CalculateAllowedIslandsTo() {
		allowedIslands = Library.Map_GetBridgetIslands(Sh.In.GameContext, fromIsland, Sh.GameState.currentUser);
	}

	void UIInit() {
		TabloidPanel.inst.SetText("Укажите, куда переместить солдат и сколько.");

		UIGodPanel.inst.godSprite.spriteName = "pic-ship_move";

		UIGodPanel.inst.actions[0].SetActionSprite("arrow-up");
		UIGodPanel.inst.actions[0].SetPrice(0);
		UIGodPanel.inst.actions[0].click = OnCountUp;

		UIGodPanel.inst.actions[1].SetActionSprite("arrow-down");
		UIGodPanel.inst.actions[1].SetPrice(0);
		UIGodPanel.inst.actions[1].click = OnCountDown;

		UIGodPanel.inst.actions[2].SetActionSprite("exit");
		UIGodPanel.inst.actions[2].SetPrice(0);
		UIGodPanel.inst.actions[2].click = OnClickCancel;

		UIGodPanel.inst.SetAdditionalText("" + count);
	}

	void CloseEventer() {
		Sh.GameState.mapStates.SetEventorType(MapEventerType.DEFAULT);
		UIGodPanel.inst.Reset();
	}
}
