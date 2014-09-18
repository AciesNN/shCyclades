using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public struct UIGodConsts {
	public Color color;
	public string spriteName;
}

public static class UIConsts {

	public static readonly Dictionary<int, Color> userColors = new Dictionary<int, Color>
	{
		{-1, Color.white},
		{0, Color.green},
		{1, Color.red},
		{2, Color.black},
		{3, Color.blue},
		{4, Color.yellow}
	};

	public static readonly Dictionary<string, Color> buildColors = new Dictionary<string, Color>
	{
		{Cyclades.Game.Constants.buildNone, Color.gray},
		{Cyclades.Game.Constants.buildMarina, Color.blue},
		{Cyclades.Game.Constants.buildFortres, Color.red},
		{Cyclades.Game.Constants.buildUniver, Color.white},
		{Cyclades.Game.Constants.buildTemple, Color.magenta}
	};

	public static readonly Dictionary<string, Color> godColors = new Dictionary<string, Color>
	{
		{Cyclades.Game.Constants.godNone, Color.black},
		{Cyclades.Game.Constants.godPoseidon, Color.blue},
		{Cyclades.Game.Constants.godMars, Color.red},
		{Cyclades.Game.Constants.godSophia, Color.gray},
		{Cyclades.Game.Constants.godZeus, Color.magenta},
		{Cyclades.Game.Constants.godAppolon, Color.white}
	};

	public static readonly Dictionary<string, string> godSprites = new Dictionary<string, string>
	{
		{Cyclades.Game.Constants.godNone, ""},
		{Cyclades.Game.Constants.godPoseidon, "Poseudon"},
		{Cyclades.Game.Constants.godMars, "Mars"},
		{Cyclades.Game.Constants.godSophia, "Sothia"},
		{Cyclades.Game.Constants.godZeus, "Zeus"},
		{Cyclades.Game.Constants.godAppolon, "coin_icon"}
	};

	public static readonly Dictionary<string, string> buildSprites = new Dictionary<string, string>
	{
		{Cyclades.Game.Constants.buildNone, "GridSquare"},
		{Cyclades.Game.Constants.buildMarina, "Port"},
		{Cyclades.Game.Constants.buildFortres, "Fort"},
		{Cyclades.Game.Constants.buildUniver, "Univer"},
		{Cyclades.Game.Constants.buildTemple, "Templ"}
	};

	public static readonly Dictionary<string, string> cardIconSprites = new Dictionary<string, string>
	{
		{Cyclades.Game.Constants.cardNone, "empty"},
		{Cyclades.Game.Constants.cardChimera, "chimera"},
		{Cyclades.Game.Constants.cardDryads, "dryad"},
		{Cyclades.Game.Constants.cardSphinx, "sphinx"},
		{Cyclades.Game.Constants.cardKraken, "kraken"},
		{Cyclades.Game.Constants.cardGorgon, "medusa"},

		{Cyclades.Game.Constants.cardMermaid, "empty"},
		{Cyclades.Game.Constants.cardPegus, "empty"},
		{Cyclades.Game.Constants.cardGigant, "empty"},
		{Cyclades.Game.Constants.cardCyclope, "empty"},
		{Cyclades.Game.Constants.cardSylph, "empty"},
		{Cyclades.Game.Constants.cardHarpy, "empty"},
		{Cyclades.Game.Constants.cardGriffon, "empty"},
		{Cyclades.Game.Constants.cardMoyra, "empty"},
		{Cyclades.Game.Constants.cardSatyr, "empty"},
		{Cyclades.Game.Constants.cardMinotaur, "empty"},
		{Cyclades.Game.Constants.cardChiron, "empty"},
		{Cyclades.Game.Constants.cardPolypheme, "empty"}

	};

	public static readonly Dictionary<string, MapEventerType> cardsMapEventors = new Dictionary<string, MapEventerType>
	{
		{Cyclades.Game.Constants.cardNone, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardSylph, MapEventerType.CARDSYL},
		{Cyclades.Game.Constants.cardPolypheme, MapEventerType.CARDPOL},
		{Cyclades.Game.Constants.cardPegus, MapEventerType.CARDPEG},

		{Cyclades.Game.Constants.cardChimera, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardDryads, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardSphinx, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardKraken, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardGorgon, MapEventerType.DEFAULT},		
		{Cyclades.Game.Constants.cardMermaid, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardGigant, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardCyclope, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardHarpy, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardGriffon, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardMoyra, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardSatyr, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardMinotaur, MapEventerType.DEFAULT},
		{Cyclades.Game.Constants.cardChiron, MapEventerType.DEFAULT}		
	};
}
