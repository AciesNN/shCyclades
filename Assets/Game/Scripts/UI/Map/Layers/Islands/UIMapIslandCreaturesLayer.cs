using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Cyclades.Game;

public class UIMapIslandCreaturesLayer : UIMapGridLayer {

	List<string> c_types = new List<string> {
		"Minotaur", "Chiron", "Gorgon", "Polypheme"
	};
	Dictionary<string, UIMapCreatureElement> creatures = new Dictionary<string, UIMapCreatureElement>();

	public override void CreateGridElements() {
		foreach (string ct in c_types)
			creatures[ct] = CreateSingleElement<UIMapCreatureElement>(GridPosition.LessThanZero());
	}

	public void GameContext_UpdateData(bool deserialize) {
		foreach (string ct in c_types) {
			int island = Sh.In.GameContext.GetInt("/creatures/{0}/island", ct);
			creatures[ct].context.SetActive(island >= 0);

			if (island >= 0) {
				GridPosition pos = new GridPosition( Sh.In.GameContext.GetList ("/map/islands/coords/[{0}]/[0]", island) );
				MoveSingleElementToPos(creatures[ct], pos);
			}
		}
	}

}