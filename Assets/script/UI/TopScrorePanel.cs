using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopScrorePanel : Panel {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnMenuBtnClicked() {
		stateMgr.State = GameState.LOBBY;
	}
}
