using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameState {
	LOBBY,
    STARTING,
	PLAYING,
	PAUSING,
	WON,
	STOP,
	RESET
}
public class StateMgr : MonoBehaviour {
	private static GameState gameState;

    public static GameState GetState() {
        return gameState;
    }
    public static void SetState(GameState newState) {
        gameState = newState;
    }
	// Use this for initialization
	void Start () {
        gameState = GameState.STARTING;		
	}
	
	// Update is called once per frame
	void Update () {
		//if (gameState == GameState.RESET) {
		//	gameState = GameState.PLAYING;
		//	return;
		//}
		Debug.Log ("GameState: " + gameState);		
	}
	
}
