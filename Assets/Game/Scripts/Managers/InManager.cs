﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Shmipl.Base;

public class InManager : Manager<InManager> {

	const string _testData = "{\"random_seed\":0,\"players_number\":5,\"turn\":{\"Zeus\":{\"buyPriestByTurn\":0},\"Sophia\":{\"buyPhilosopherByTurn\":0},\"Appolo\":{\"getHorn\":0},\"Mars\":{\"buyArmyByTurn\":0},\"current_god\":\"M\",\"current_player\":3,\"player_order\":[0,1,4,2],\"Poseidon\":{\"buyNavyByTurn\":0}},\"counter\":233,\"cards\":{\"trash\":[],\"open_card_number\":-1,\"open\":[\"Gor\",\"Pol\"],\"stack\":[\"Mer\",\"Peg\",\"Gig\",\"Chm\",\"Cyc\",\"Sph\",\"Syl\",\"Har\",\"Gri\",\"Moy\",\"Sat\",\"Dry\",\"Kra\",\"Min\",\"Chr\"],\"need_refresh_stack\":false},\"fight\":{\"army\":{\"fight\":false,\"attacker\":{\"retreat_way\":false,\"island\":0,\"player\":0,\"units\":0},\"deffender\":{\"retreat_way\":false,\"fortress\":0,\"island\":0,\"player\":0,\"units\":0}},\"navy\":{\"fight\":false,\"last_coords\":[-1,-1],\"deffender\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0},\"move\":0,\"attacker\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0}}},\"markers\":{\"income\":[3,2,2,3,2],\"philosopher\":[0,2,0,0,0],\"priest\":[0,0,1,0,0],\"gold\":[10,2,6,5,5]},\"map\":{\"islands\":{\"horn\":[2,0,1,1,1,2,1,2,0,0,2,0,0],\"is_metro\":[false,false,false,false,false,false,false,false,false,false,false,false,false],\"coords\":[[[3,0],[4,1]],[[0,1],[1,1],[0,2],[1,2]],[[3,3],[4,4]],[[5,3],[6,3]],[[8,3],[8,4]],[[1,4]],[[3,5],[3,6]],[[6,5]],[[0,6],[0,7],[1,7]],[[9,6],[8,7],[7,8],[6,9]],[[6,7]],[[2,8],[1,9],[2,9]],[[4,8],[4,9],[4,10]]],\"owners\":[0,1,2,3,3,4,-1,1,3,4,-1,0,2],\"buildings\":[[\"\",\"\"],[\"\",\"\",\"\",\"\"],[\"\",\"\"],[\"\",\"\"],[\"\",\"M\"],[\"\"],[\"\",\"\"],[\"\"],[\"\",\"\"],[\"P\",\"\",\"\",\"\"],[\"\"],[\"\",\"\",\"\"],[\"\",\"Z\",\"\"]],\"army\":[1,1,1,1,0,1,0,1,1,1,0,1,1]},\"size_x\":6,\"size_y\":6,\"seas\":{\"horns\":[[0,0],[5,0],[0,5],[10,5],[0,10],[5,10]],\"ships\":{\"coords\":[[5,2],[0,10],[2,1],[6,4],[4,5],[5,10],[7,3],[0,5],[2,5],[8,6]],\"owners\":[0,0,1,1,2,2,3,3,4,4]}}},\"auction\":{\"start_order\":[0,2,1,3,4],\"player_order\":[],\"gods_order\":[\"P\",\"M\",\"S\",\"Z\",\"A\"],\"bets\":[[0,0,0,1,0],[1,0,0,0,0],[0,1,0,0,0],[0,0,0,0,1],[0,0,1,0,0]],\"current_player\":4},\"cur_state\":\"Turn.PlaceBuild\",\"creatures\":{\"Polypheme\":{\"island\":-1,\"player\":-1},\"Gorgon\":{\"island\":-1,\"player\":-1},\"Kraken\":{\"coords\":[-1,-1]},\"Chiron\":{\"island\":-1,\"player\":-1},\"Minotaur\":{\"island\":-1,\"player\":-1}}}";
	public GameObject rootUI;

	Dictionary<string, IContextGet> _test_contexts = new Dictionary<string,IContextGet>();

	override protected void Init() {
		//TODO подписаться на события контекстов - для обновления
	}

	void Start() {
		_LoadContextFromText(_testData);
	}

	public IContextGet GameContext {
		get { return _test_contexts["Game"]; } 
	}

	public void _LoadContextFromText(string text) {
		bool newContext = !_test_contexts.ContainsKey("Game");
		if (newContext) {
			_test_contexts["Game"] = new Context();
		}

		Context c = GameContext as Context;
		c.LoadDataFromText(text);

		if (newContext)
			rootUI.BroadcastMessage("GameContext_LateInit", SendMessageOptions.DontRequireReceiver);

		rootUI.BroadcastMessage("GameContext_UpdateData", SendMessageOptions.DontRequireReceiver);
		Sh.GameState.GameContext_UpdateData();
	}

	public void UpdateData() {
		//TODO тут можно проанализировать - есть ли данные, если есть то подготовлены ли для рассылки (FSM) и т.д.
		//залочить данные 
		//рассылка
		rootUI.BroadcastMessage("GameContext_UpdateData", SendMessageOptions.DontRequireReceiver);
	}

}
