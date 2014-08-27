using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Shmipl.Base;

public class InManager : Manager<InManager> {

	public GameObject rootUI;

	Dictionary<string, IContextGet> _test_contexts = new Dictionary<string,IContextGet>();

	void Start() {
		//TODO подписаться на события контекстов - для обновления
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
