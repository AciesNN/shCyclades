using UnityEngine;
using System.Collections;

public class InfoPanel : MonoBehaviour {

	public UISprite server;
	public UISprite game;
	public UISprite net;
	public UISprite client;
	public UISprite[] users;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SetIconState(server, Cyclades.Program.srv != null);
		SetIconState(game, Cyclades.Program.srv != null);
		SetIconState(net, Cyclades.Program.srv != null);
		SetIconState(client, Cyclades.Program.clnt != null);

		for (int i = 0; i < 5; ++i) {
			SetIconState(users[i], Cyclades.Program.clnts.Count > i);
			if (Cyclades.Program.clnts.Count > i) {
				//users[i].spriteName = 
			}
		}

	}

	void SetIconState(UISprite icon, bool state) {
		icon.color = (state ? Color.white : Color.black);
	}
}
