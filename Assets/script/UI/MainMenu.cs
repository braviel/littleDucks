using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Panel {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnStartBtnClicked(Button sender)
	{
        Debug.Log("Start Btn clicked");
        StateMgr.instance.State = GameState.LEVEL_CHOSE;
	}
}
