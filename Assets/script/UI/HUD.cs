using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Panel {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void onPauseClicked(Button sender)
	{
		Debug.Log("Start button clicked");
		Text btnText = sender.GetComponentInChildren<Text>();

		if (stateMgr.State == GameState.PLAYING)
		{
			stateMgr.State = GameState.PAUSING;
			//btnText.text = "Resume";
		}
	}
	public void onRetryClicked(Button sender)
	{		
		stateMgr.State = GameState.RESET;
	}
	public void onNextClicked(Button sender)
	{
		stateMgr.NextLevel();
	}
}
