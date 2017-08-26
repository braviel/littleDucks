using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGM : Panel {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MainMenuBtnClicked() {
		stateMgr.State = GameState.LOBBY;
    }

	public void ResumeBtnClicked()
	{
		stateMgr.State = GameState.PLAYING;
	}
}
