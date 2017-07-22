using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMgr : MonoBehaviour {
	public GameObject winDialog;
	public GameObject startBtn;
	public GameObject resetBtn;

	private GameState gameState;
	void Start () {
		winDialog.SetActive (false);
	}

	void Update () {
        gameState = StateMgr.GetState();
		if (gameState == GameState.WON) {			
			winDialog.SetActive (true);
		}
	}
//	public void showWinDialog() {
//		winDialog.SetActive (true);
//	}
	public void onPauseClicked(Button sender) {
		Debug.Log ("Start button clicked");
		Text btnText = sender.GetComponentInChildren<Text> ();

		if (gameState == GameState.PLAYING) {
            StateMgr.SetState(GameState.PAUSING);
			btnText.text = "Resume";
		} else if(gameState == GameState.PAUSING){
            StateMgr.SetState(GameState.PLAYING);
			btnText.text = "Pause";
		}
	}
	public void onRetryClicked(Button sender) {
		winDialog.SetActive (false);
        StateMgr.SetState(GameState.RESET);
	}
}
