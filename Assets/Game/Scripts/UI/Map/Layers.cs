using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GridLayerType {
	//ДОБАВЛЯТЬ ТОЛЬКО В КОНЕЦ

	ERROR,
	GRID,
	ISLANDS,
	SKY,
	BUILDINGS,
	UNITS,
	SHIPS,
	HORNS,
	CREATURES

	//ДОБАВЛЯТЬ ТОЛЬКО В КОНЕЦ
}

static public class MapLayersDepths {
	static Dictionary<GridLayerType, int> depths = new Dictionary<GridLayerType, int>
	{

		//{GridLayerType.BACKGROUND, 100},

		{GridLayerType.ISLANDS, 200},
		{GridLayerType.BUILDINGS, 300},
		
		{GridLayerType.UNITS, 400},
		
		{GridLayerType.HORNS, 300},
		{GridLayerType.SHIPS, 400},
		{GridLayerType.CREATURES, 400},

		{GridLayerType.GRID, 1000},

		{GridLayerType.SKY, 2000}

	};
	
	static public int GetDepth(GridLayerType type) {
		return depths[type];
	}
}

