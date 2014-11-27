using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Shmipl.Base;

public class InManager : Manager<InManager> {

	public Dictionary<string, string> _testDataCash = new Dictionary<string, string> {
		{"0", "{\"random_seed\":0,\"players_number\":5,\"turn\":{\"Zeus\":{\"buyPriestByTurn\":0},\"Sophia\":{\"buyPhilosopherByTurn\":0},\"Appolo\":{\"getHorn\":0},\"Mars\":{\"buyArmyByTurn\":0},\"current_god\":\"M\",\"current_player\":3,\"player_order\":[0,1,4,2],\"Poseidon\":{\"buyNavyByTurn\":0}},\"counter\":233,\"cards\":{\"trash\":[],\"open_card_number\":-1,\"open\":[\"Gor\",\"Pol\"],\"stack\":[\"Mer\",\"Peg\",\"Gig\",\"Chm\",\"Cyc\",\"Sph\",\"Syl\",\"Har\",\"Gri\",\"Moy\",\"Sat\",\"Dry\",\"Kra\",\"Min\",\"Chr\"],\"need_refresh_stack\":false},\"fight\":{\"army\":{\"fight\":false,\"attacker\":{\"retreat_way\":true,\"island\":0,\"player\":0,\"units\":4},\"deffender\":{\"retreat_way\":false,\"fortress\":1,\"island\":0,\"player\":0,\"units\":3}},\"navy\":{\"fight\":false,\"last_coords\":[-1,-1],\"deffender\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0},\"move\":0,\"attacker\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0}}},\"markers\":{\"income\":[3,2,2,3,2],\"philosopher\":[0,2,0,0,0],\"priest\":[0,0,1,0,0],\"gold\":[10,2,6,5,5]},\"map\":{\"islands\":{\"horn\":[2,0,1,1,1,2,1,2,0,0,2,0,0],\"is_metro\":[false,false,false,false,false,false,false,false,false,false,false,false,false],\"coords\":[[[3,0],[4,1]],[[0,1],[1,1],[0,2],[1,2]],[[3,3],[4,4]],[[5,3],[6,3]],[[8,3],[8,4]],[[1,4]],[[3,5],[3,6]],[[6,5]],[[0,6],[0,7],[1,7]],[[9,6],[8,7],[7,8],[6,9]],[[6,7]],[[2,8],[1,9],[2,9]],[[4,8],[4,9],[4,10]]],\"owners\":[0,1,2,3,3,4,-1,1,3,4,-1,0,2],\"buildings\":[[\"\",\"\"],[\"\",\"\",\"\",\"\"],[\"\",\"\"],[\"\",\"\"],[\"\",\"M\"],[\"\"],[\"\",\"\"],[\"\"],[\"\",\"\"],[\"P\",\"\",\"\",\"\"],[\"\"],[\"\",\"\",\"\"],[\"\",\"Z\",\"\"]],\"army\":[1,1,1,1,0,1,0,1,1,1,0,1,1]},\"size_x\":6,\"size_y\":6,\"seas\":{\"horns\":[[0,0],[5,0],[0,5],[10,5],[0,10],[5,10]],\"ships\":{\"coords\":[[5,2],[0,10],[2,1],[6,4],[4,5],[5,10],[7,3],[0,5],[2,5],[8,6]],\"owners\":[0,0,1,1,2,2,3,3,4,4]}}},\"auction\":{\"start_order\":[0,2,1,3,4],\"player_order\":[],\"gods_order\":[\"P\",\"M\",\"S\",\"Z\",\"A\"],\"bets\":[[0,0,0,1,0],[1,0,0,0,0],[0,1,0,0,0],[0,0,0,0,1],[0,0,1,0,0]],\"current_player\":4},\"cur_state\":\"Turn.PlaceBuild\",\"creatures\":{\"Polypheme\":{\"island\":-1,\"player\":-1},\"Gorgon\":{\"island\":-1,\"player\":-1},\"Kraken\":{\"coords\":[-1,-1]},\"Chiron\":{\"island\":-1,\"player\":-1},\"Minotaur\":{\"island\":-1,\"player\":-1}}}"},
		{"1", "{\"random_seed\":0,\"players_number\":5,\"turn\":{\"Zeus\":{\"buyPriestByTurn\":0},\"Sophia\":{\"buyPhilosopherByTurn\":0},\"Appolo\":{\"getHorn\":0},\"Mars\":{\"buyArmyByTurn\":0},\"current_god\":\"P\",\"current_player\":3,\"player_order\":[0,1,4,2],\"Poseidon\":{\"buyNavyByTurn\":0}},\"counter\":233,\"cards\":{\"trash\":[],\"open_card_number\":-1,\"open\":[\"Gor\",\"Pol\"],\"stack\":[\"Mer\",\"Peg\",\"Gig\",\"Chm\",\"Cyc\",\"Sph\",\"Syl\",\"Har\",\"Gri\",\"Moy\",\"Sat\",\"Dry\",\"Kra\",\"Min\",\"Chr\"],\"need_refresh_stack\":false},\"fight\":{\"army\":{\"fight\":false,\"attacker\":{\"retreat_way\":false,\"island\":0,\"player\":0,\"units\":0},\"deffender\":{\"retreat_way\":false,\"fortress\":0,\"island\":0,\"player\":0,\"units\":0}},\"navy\":{\"fight\":false,\"last_coords\":[-1,-1],\"deffender\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0},\"move\":0,\"attacker\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0}}},\"markers\":{\"income\":[3,2,2,3,2],\"philosopher\":[0,2,0,0,0],\"priest\":[0,0,1,0,0],\"gold\":[10,2,6,5,5]},\"map\":{\"islands\":{\"horn\":[2,0,1,1,1,2,1,2,0,0,2,0,0],\"is_metro\":[false,false,false,false,false,false,false,false,false,false,false,false,false],\"coords\":[[[3,0],[4,1]],[[0,1],[1,1],[0,2],[1,2]],[[3,3],[4,4]],[[5,3],[6,3]],[[8,3],[8,4]],[[1,4]],[[3,5],[3,6]],[[6,5]],[[0,6],[0,7],[1,7]],[[9,6],[8,7],[7,8],[6,9]],[[6,7]],[[2,8],[1,9],[2,9]],[[4,8],[4,9],[4,10]]],\"owners\":[0,1,2,3,3,4,-1,1,3,4,-1,0,2],\"buildings\":[[\"M\",\"\"],[\"\",\"S\",\"\",\"\"],[\"\",\"\"],[\"P\",\"\"],[\"\",\"M\"],[\"\"],[\"\",\"\"],[\"\"],[\"\",\"\"],[\"P\",\"\",\"\",\"\"],[\"\"],[\"\",\"\",\"\"],[\"\",\"Z\",\"\"]],\"army\":[1,1,1,1,0,1,0,1,1,1,0,1,1]},\"size_x\":6,\"size_y\":6,\"seas\":{\"horns\":[[0,0],[5,0],[0,5],[10,5],[0,10],[5,10]],\"ships\":{\"coords\":[[5,2],[0,10],[2,1],[6,4],[4,5],[5,10],[7,3],[0,5],[2,5],[8,6]],\"owners\":[0,0,1,1,2,2,3,3,4,4]}}},\"auction\":{\"start_order\":[0,2,1,3,4],\"player_order\":[],\"gods_order\":[\"P\",\"M\",\"S\",\"Z\",\"A\"],\"bets\":[[0,0,0,1,0],[1,0,0,0,0],[0,1,0,0,0],[0,0,0,0,1],[0,0,1,0,0]],\"current_player\":4},\"cur_state\":\"Turn.PlaceBuild\",\"creatures\":{\"Polypheme\":{\"island\":-1,\"player\":-1},\"Gorgon\":{\"island\":-1,\"player\":-1},\"Kraken\":{\"coords\":[-1,-1]},\"Chiron\":{\"island\":-1,\"player\":-1},\"Minotaur\":{\"island\":-1,\"player\":-1}}}"},
		{"2", "{\"random_seed\":0,\"players_number\":5,\"turn\":{\"Zeus\":{\"buyPriestByTurn\":0},\"Appolo\":{\"getHorn\":0},\"player_order\":[0,3],\"Mars\":{\"buyArmyByTurn\":0},\"current_god\":\"S\",\"current_player\":1,\"Sophia\":{\"buyPhilosopherByTurn\":1},\"Poseidon\":{\"buyNavyByTurn\":1}},\"counter\":418,\"cards\":{\"trash\":[],\"open_card_number\":-1,\"open\":[\"Chr\",\"Gor\",\"Pol\"],\"stack\":[\"Mer\",\"Peg\",\"Gig\",\"Chm\",\"Cyc\",\"Sph\",\"Syl\",\"Har\",\"Gri\",\"Moy\",\"Sat\",\"Dry\",\"Kra\",\"Min\"],\"need_refresh_stack\":false},\"fight\":{\"army\":{\"fight\":false,\"attacker\":{\"retreat_way\":false,\"island\":0,\"player\":0,\"units\":0},\"deffender\":{\"retreat_way\":false,\"fortress\":0,\"island\":0,\"player\":0,\"units\":0}},\"navy\":{\"fight\":false,\"last_coords\":[-1,-1],\"deffender\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0},\"move\":0,\"attacker\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0}}},\"markers\":{\"income\":[3,2,3,3,2],\"philosopher\":[0,-4,0,0,0],\"priest\":[0,0,1,0,1],\"gold\":[10,3,7,6,1]},\"map\":{\"islands\":{\"horn\":[2,0,2,1,1,2,1,2,0,0,2,0,0],\"is_metro\":[false,true,false,false,false,false,false,false,false,false,false,false,false],\"coords\":[[[3,0],[4,1]],[[0,1],[1,1],[0,2],[1,2]],[[3,3],[4,4]],[[5,3],[6,3]],[[8,3],[8,4]],[[1,4]],[[3,5],[3,6]],[[6,5]],[[0,6],[0,7],[1,7]],[[9,6],[8,7],[7,8],[6,9]],[[6,7]],[[2,8],[1,9],[2,9]],[[4,8],[4,9],[4,10]]],\"owners\":[0,1,2,3,3,4,4,1,3,4,-1,0,2],\"buildings\":[[\"M\",\"\"],[\"\",\"\",\"\",\"\"],[\"P\",\"\"],[\"P\",\"\"],[\"\",\"M\"],[\"\"],[\"\",\"\"],[\"\"],[\"\",\"\"],[\"P\",\"Z\",\"M\",\"\"],[\"\"],[\"\",\"\",\"\"],[\"\",\"Z\",\"\"]],\"army\":[1,1,1,1,0,0,1,1,1,1,0,1,1]},\"size_x\":6,\"size_y\":6,\"seas\":{\"horns\":[[0,0],[5,0],[0,5],[10,5],[0,10],[5,10]],\"ships\":{\"coords\":[[5,2],[0,10],[2,1],[6,4],[4,5],[5,10],[7,3],[0,5],[2,5],[8,6],[4,3]],\"owners\":[0,0,1,1,2,2,3,3,4,4,2]}}},\"auction\":{\"start_order\":[2,4,1,0,3],\"player_order\":[4,2],\"gods_order\":[\"P\",\"M\",\"S\",\"Z\",\"A\"],\"bets\":[[0,0,2,0,0],[0,0,0,0,1],[0,1,0,0,0],[1,0,0,0,0],[0,0,0,1,0]],\"current_player\":3},\"cur_state\":\"Turn.Turn\",\"creatures\":{\"Polypheme\":{\"island\":-1,\"player\":-1},\"Gorgon\":{\"island\":-1,\"player\":-1},\"Kraken\":{\"coords\":[-1,-1]},\"Chiron\":{\"island\":-1,\"player\":-1},\"Minotaur\":{\"island\":-1,\"player\":-1}}}"},
		{"3", "{\"random_seed\":0,\"players_number\":5,\"turn\":{\"Zeus\":{\"buyPriestByTurn\":0},\"Appolo\":{\"getHorn\":0},\"player_order\":[1,2],\"Mars\":{\"buyArmyByTurn\":0},\"current_god\":\"S\",\"current_player\":4,\"Sophia\":{\"buyPhilosopherByTurn\":0},\"Poseidon\":{\"buyNavyByTurn\":0}},\"counter\":699,\"cards\":{\"trash\":[\"Pol\",\"Gor\"],\"open_card_number\":-1,\"open\":[\"Kra\",\"Min\",\"Chr\"],\"stack\":[\"Mer\",\"Peg\",\"Gig\",\"Chm\",\"Cyc\",\"Sph\",\"Syl\",\"Har\",\"Gri\",\"Moy\",\"Sat\",\"Dry\"],\"need_refresh_stack\":false},\"fight\":{\"army\":{\"fight\":false,\"attacker\":{\"retreat_way\":false,\"island\":0,\"player\":0,\"units\":0},\"deffender\":{\"retreat_way\":false,\"fortress\":0,\"island\":0,\"player\":0,\"units\":0}},\"navy\":{\"fight\":false,\"last_coords\":[-1,-1],\"deffender\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0},\"move\":0,\"attacker\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0}}},\"markers\":{\"income\":[3,2,4,4,3],\"philosopher\":[0,-4,0,2,0],\"priest\":[1,0,1,0,2],\"gold\":[12,4,15,7,3]},\"map\":{\"islands\":{\"horn\":[2,0,3,2,1,2,1,2,0,0,2,0,0],\"is_metro\":[false,true,false,false,false,false,false,false,false,false,false,false,false],\"coords\":[[[3,0],[4,1]],[[0,1],[1,1],[0,2],[1,2]],[[3,3],[4,4]],[[5,3],[6,3]],[[8,3],[8,4]],[[1,4]],[[3,5],[3,6]],[[6,5]],[[0,6],[0,7],[1,7]],[[9,6],[8,7],[7,8],[6,9]],[[6,7]],[[2,8],[1,9],[2,9]],[[4,8],[4,9],[4,10]]],\"owners\":[0,1,2,3,3,4,4,1,3,4,-1,0,2],\"buildings\":[[\"M\",\"Z\"],[\"\",\"\",\"\",\"\"],[\"P\",\"\"],[\"P\",\"\"],[\"S\",\"M\"],[\"\"],[\"\",\"Z\"],[\"\"],[\"\",\"\"],[\"P\",\"Z\",\"M\",\"\"],[\"\"],[\"\",\"\",\"\"],[\"\",\"Z\",\"\"]],\"army\":[1,1,1,1,0,0,1,1,1,1,0,2,1]},\"size_x\":6,\"size_y\":6,\"seas\":{\"horns\":[[0,0],[5,0],[0,5],[10,5],[0,10],[5,10]],\"ships\":{\"coords\":[[5,2],[0,10],[2,1],[6,4],[4,5],[5,10],[7,3],[0,5],[2,5],[8,6],[4,3],[6,6]],\"owners\":[0,0,1,1,2,2,3,3,4,4,2,1]}}},\"auction\":{\"start_order\":[2,4,3,0,1],\"player_order\":[0,3],\"gods_order\":[\"P\",\"M\",\"S\",\"Z\",\"A\"],\"bets\":[[0,0,0,1,0],[1,0,0,0,0],[0,0,0,0,1],[0,1,0,0,0],[0,0,2,0,0]],\"current_player\":1},\"cur_state\":\"Turn.Turn\",\"creatures\":{\"Polypheme\":{\"island\":-1,\"player\":-1},\"Gorgon\":{\"island\":-1,\"player\":-1},\"Kraken\":{\"coords\":[-1,-1]},\"Chiron\":{\"island\":-1,\"player\":-1},\"Minotaur\":{\"island\":-1,\"player\":-1}}}"},
		{"4", "{\"random_seed\":0,\"players_number\":5,\"turn\":{\"Mars\":{\"buyArmyByTurn\":0},\"Appolo\":{\"getHorn\":0},\"player_order\":[],\"current_god\":\"\",\"Zeus\":{\"buyPriestByTurn\":0},\"Poseidon\":{\"buyNavyByTurn\":0},\"current_player\":-1,\"Sophia\":{\"buyPhilosopherByTurn\":0}},\"counter\":54,\"cards\":{\"trash\":[],\"open_card_number\":-1,\"open\":[\"Pol\"],\"stack\":[\"Mer\",\"Peg\",\"Gig\",\"Chm\",\"Cyc\",\"Sph\",\"Syl\",\"Har\",\"Gri\",\"Moy\",\"Sat\",\"Dry\",\"Kra\",\"Min\",\"Chr\",\"Gor\"],\"need_refresh_stack\":false},\"fight\":{\"army\":{\"fight\":false,\"attacker\":{\"retreat_way\":false,\"island\":0,\"player\":0,\"units\":0},\"deffender\":{\"retreat_way\":false,\"fortress\":0,\"island\":0,\"player\":0,\"units\":0}},\"navy\":{\"fight\":false,\"last_coords\":[-1,-1],\"deffender\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0},\"move\":0,\"attacker\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0}}},\"markers\":{\"income\":[2,2,2,2,2],\"philosopher\":[0,0,0,0,0],\"priest\":[0,0,0,0,0],\"gold\":[7,7,7,7,7]},\"map\":{\"islands\":{\"horn\":[1,0,1,1,1,2,1,2,0,0,2,0,0],\"is_metro\":[false,false,false,false,false,false,false,false,false,false,false,false,false],\"coords\":[[[3,0],[4,1]],[[0,1],[1,1],[0,2],[1,2]],[[3,3],[4,4]],[[5,3],[6,3]],[[8,3],[8,4]],[[1,4]],[[3,5],[3,6]],[[6,5]],[[0,6],[0,7],[1,7]],[[9,6],[8,7],[7,8],[6,9]],[[6,7]],[[2,8],[1,9],[2,9]],[[4,8],[4,9],[4,10]]],\"owners\":[0,1,2,-1,3,4,-1,1,3,4,-1,0,2],\"buildings\":[[\"\",\"\"],[\"\",\"\",\"\",\"\"],[\"\",\"\"],[\"\",\"\"],[\"\",\"\"],[\"\"],[\"\",\"\"],[\"\"],[\"\",\"\"],[\"\",\"\",\"\",\"\"],[\"\"],[\"\",\"\",\"\"],[\"\",\"\",\"\"]],\"army\":[1,1,1,0,1,1,0,1,1,1,0,1,1]},\"size_x\":6,\"size_y\":6,\"seas\":{\"horns\":[[0,0],[5,0],[0,5],[10,5],[0,10],[5,10]],\"ships\":{\"coords\":[[5,2],[0,10],[2,1],[6,4],[4,5],[5,10],[7,3],[0,5],[2,5],[8,6]],\"owners\":[0,0,1,1,2,2,3,3,4,4]}}},\"auction\":{\"start_order\":[4,3,2,1,0],\"player_order\":[],\"gods_order\":[\"P\",\"M\",\"S\",\"Z\",\"A\"],\"bets\":[[0,0,0,0,1],[0,0,0,1,0],[0,0,1,0,0],[0,1,0,0,0],[0,0,0,0,0]],\"current_player\":0},\"cur_state\":\"Auction.Bet\",\"creatures\":{\"Polypheme\":{\"island\":-1,\"player\":-1},\"Gorgon\":{\"island\":-1,\"player\":-1},\"Kraken\":{\"coords\":[-1,-1]},\"Chiron\":{\"island\":-1,\"player\":-1},\"Minotaur\":{\"island\":-1,\"player\":-1}}}"},
		{"5", "{\"random_seed\":0,\"players_number\":5,\"turn\":{\"Zeus\":{\"buyPriestByTurn\":1},\"Sophia\":{\"buyPhilosopherByTurn\":1},\"Appolo\":{\"getHorn\":1},\"Mars\":{\"buyArmyByTurn\":0},\"current_god\":\"A\",\"current_player\":1,\"player_order\":[],\"Poseidon\":{\"buyNavyByTurn\":0}},\"counter\":975,\"cards\":{\"trash\":[\"Pol\",\"Gor\",\"Chr\"],\"open_card_number\":-1,\"open\":[\"Dry\",\"Kra\",\"Min\"],\"stack\":[\"Mer\",\"Peg\",\"Gig\",\"Chm\",\"Cyc\",\"Sph\",\"Syl\",\"Har\",\"Gri\",\"Moy\",\"Sat\"],\"need_refresh_stack\":false},\"fight\":{\"army\":{\"fight\":false,\"attacker\":{\"retreat_way\":false,\"island\":0,\"player\":0,\"units\":0},\"deffender\":{\"retreat_way\":false,\"fortress\":0,\"island\":0,\"player\":0,\"units\":0}},\"navy\":{\"fight\":false,\"last_coords\":[5,0],\"deffender\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0},\"move\":2,\"attacker\":{\"retreat_way\":false,\"marines\":0,\"coords\":[],\"player\":0,\"units\":0}}},\"markers\":{\"income\":[6,6,4,3,3],\"philosopher\":[1,0,2,1,-4],\"priest\":[0,1,1,3,1],\"gold\":[23,16,10,7,3]},\"map\":{\"islands\":{\"horn\":[4,0,1,1,1,2,1,4,0,0,2,0,1],\"is_metro\":[false,false,false,false,true,true,false,false,false,true,false,false,false],\"coords\":[[[3,0],[4,1]],[[0,1],[1,1],[0,2],[1,2]],[[3,3],[4,4]],[[5,3],[6,3]],[[8,3],[8,4]],[[1,4]],[[3,5],[3,6]],[[6,5]],[[0,6],[0,7],[1,7]],[[9,6],[8,7],[7,8],[6,9]],[[6,7]],[[2,8],[1,9],[2,9]],[[4,8],[4,9],[4,10]]],\"owners\":[0,1,2,3,3,4,2,1,3,4,1,0,2],\"buildings\":[[\"P\",\"\"],[\"Z\",\"P\",\"\",\"\"],[\"S\",\"Z\"],[\"\",\"\"],[\"\",\"\"],[\"\"],[\"M\",\"\"],[\"M\"],[\"\",\"\"],[\"\",\"\",\"\",\"\"],[\"P\"],[\"M\",\"\",\"\"],[\"\",\"\",\"\"]],\"army\":[1,1,1,1,1,1,1,1,1,1,1,1,1]},\"size_x\":6,\"size_y\":6,\"seas\":{\"horns\":[[0,0],[5,0],[0,5],[10,5],[0,10],[5,10]],\"ships\":{\"coords\":[[5,2],[0,10],[6,4],[4,5],[5,10],[7,3],[0,5],[2,5],[10,5],[7,7],[6,6],[0,0],[5,0]],\"owners\":[0,0,1,2,2,3,3,4,4,4,1,1,0]}}},\"auction\":{\"start_order\":[2,0,3,4,1],\"player_order\":[3,0,4,2],\"gods_order\":[\"P\",\"M\",\"S\",\"Z\",\"A\"],\"bets\":[[0,0,1,0,0],[0,0,0,0,1],[1,0,0,0,0],[0,0,0,1,0],[0,1,0,0,0]],\"current_player\":1},\"cur_state\":\"Turn.Turn\",\"creatures\":{\"Polypheme\":{\"island\":-1,\"player\":-1},\"Gorgon\":{\"island\":-1,\"player\":-1},\"Kraken\":{\"coords\":[-1,-1]},\"Chiron\":{\"island\":-1,\"player\":-1},\"Minotaur\":{\"island\":-1,\"player\":-1}}}"}
	};

	bool _is_init_game_context = false;

	override protected void Init() {
		base.Init();

		Shmipl.Base.Messenger<Hashtable, long, bool, bool>.AddListener("UnityShmipl.UpdateView", UpdateGameData);
	}

	void Start() {
	}

	long counter = -1;
	public Context GameContext {
		get {
			if (counter == -1)
				return null;

			if (Cyclades.Program.isServer) {

				if (!Cyclades.Program.clnts.ContainsKey(Sh.Sрmipl._pl))
					return null;
				Shmipl.FrmWrk.Client.DispetcherFSM dsp = Cyclades.Program.clnts[Sh.Sрmipl._pl];
				if (dsp == null || !dsp.history_tree.ContainsKey(Sh.Sрmipl._gm))
					return null;
				Shmipl.FrmWrk.ContextHistory ch = dsp.history_tree[Sh.Sрmipl._gm];
				return ch.Get (counter); //TODO кешировать надо при изменении counter
			
			} else {

				Shmipl.FrmWrk.Client.DispetcherFSM dsp = Cyclades.Program.clnt;
				if (dsp == null || !dsp.history_tree.ContainsKey(Sh.Sрmipl._gm))
					return null;
				Shmipl.FrmWrk.ContextHistory ch = dsp.history_tree[Sh.Sрmipl._gm];
				return ch.Get (counter); //TODO кешировать надо при изменении counter

			}
		} 
	}

	public void _LoadContextFromCash(string cashNumber) {
		if (_testDataCash.ContainsKey(cashNumber)) {
			_LoadContextFromText(_testDataCash[cashNumber]);
		} else {
			NGUIDebug.Log("нет такого");
		}
	}

	public void _LoadContextFromText(string text) {
		Hashtable msg = Shmipl.Base.json.loads(text);
		Cyclades.Program.srv.Deserialize(Sh.Sрmipl._gm, msg);
	}

	//TODO 
	//видятся вот какие проблемы: 
	//-обновление контекста может случиться в нестабильный момент 
	public void UpdateGameData(Hashtable msg, long counter, bool stable, bool deserialize) {
		//будем обновлять в двух случаях: это десериализация, или это установка стабильного состояния 
		if (deserialize || stable) {
			if (this.counter >= counter)
				return;
			this.counter = counter;
			Debug.Log ("counter == " + counter);

			if (GameContext != null) {
				//TODO исключительно код для отладки (и то не всегда нужен)
				Sh.GameState.currentUser = (int)Cyclades.Game.Library.GetCurrentPlayer(GameContext);

				if (!_is_init_game_context) {
					_is_init_game_context = true;
					Sh.GameState.GameContext_LateInit();
					Sh.GameState.GameContext_UpdateData(deserialize);
				} else {
					Sh.GameState.GameContext_UpdateData(deserialize);
				}

			} else {
				NGUIDebug.Log("ERROR: контекст не обнаружен по счетчику: " + counter + " (" + deserialize + ")");
			}
		} else {
			Sh.GameState.GameContext_ShowAnimation(msg);
		}
	}

}
