using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public struct UIGodConsts {
	public Color color;
	public string spriteName;
}

public static class UIConsts {

	public static readonly List<Color> userColors = new List<Color> 
	{
		Color.green,
		Color.red,
		Color.black,
		Color.blue,
		Color.yellow
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
}
