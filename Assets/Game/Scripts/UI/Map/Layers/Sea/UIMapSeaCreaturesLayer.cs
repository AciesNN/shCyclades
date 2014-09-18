using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIMapSeaCreaturesLayer : UIMapGridLayer {

	UIMapCreatureElement creature;

	public override void CreateGridElements() {
		creature = CreateSingleElement<UIMapCreatureElement>(GridPosition.LessThanZero());
	}

	public void GameContext_UpdateData() {
		GridPosition pos = new GridPosition(Sh.In.GameContext.GetList("/creatures/Kraken/coords"));
		creature.context.SetActive(!pos.IsLessThanZero());
		if (!pos.IsLessThanZero())
			MoveSingleElementToPos(creature, pos);
	}

}