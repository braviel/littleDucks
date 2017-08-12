using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMgr : MonoBehaviour {
    public List<Panel> panelList;
	private GameState gameState;

	void Awake () {
		//SetAllPanelActive(false);
        foreach (Panel p in panelList) {
            p.Init();
        }
	}

    void SetAllPanelActive(bool active) 
    {
        Debug.Log("All panel activation: " + active);
        foreach (Panel p in panelList)
        {
            p.gameObject.SetActive(active);
        }
    }
    void ActiveOnlyPanel(string panelName) {
        Debug.Log("Active only: " + panelName);
		foreach (Panel p in panelList)
		{
            //Debug.Log("\t Curr name: " + p.gameObject.name);
            if(p.gameObject.name == panelName) {
                p.gameObject.SetActive(true);
            } else {
                p.gameObject.SetActive(false);   
            }
		}
    }
    void HidePanelName(string panelName) {
		foreach (Panel p in panelList)
		{
            if (p.gameObject.name == panelName)
			{
				p.gameObject.SetActive(false);
			}
		}
    }
	void Update () {
        
        		
	}
    public void OnChageState() {
        gameState = StateMgr.instance.State;
        Debug.Log("ON State Change " + gameState.ToString());
		switch (gameState)
		{
            case GameState.INTRO:
				{
					ActiveOnlyPanel("Intro");
					break;
				}
			case GameState.LOBBY:
				{
					ActiveOnlyPanel("MainMenu");
					break;
				}
            case GameState.LEVEL_CHOSE:
				{
                    ActiveOnlyPanel("LevelChoser");
					break;
				}
			case GameState.PAUSING:
				{
					break;
				}
			case GameState.PLAYING:
				{
                    ActiveOnlyPanel("HUD");
					break;
				}
            case GameState.BEGIN_LEVEL: 
                {
                    ActiveOnlyPanel("HUD");
                    break;
                }
			case GameState.WON_LEVEL:
				{
					ActiveOnlyPanel("WinLevel");
					break;
				}

		}
    }
	public void onPauseClicked(Button sender) {
		Debug.Log ("Start button clicked");
		Text btnText = sender.GetComponentInChildren<Text> ();

		if (gameState == GameState.PLAYING) {
            StateMgr.instance.State = GameState.PAUSING;
			//btnText.text = "Resume";
		} else if(gameState == GameState.PAUSING){
            StateMgr.instance.State = GameState.PLAYING;
            //btnText.text = "Pause";
		}
	}
	public void onRetryClicked(Button sender) {
        HidePanelName("winLevel");
        StateMgr.instance.State = GameState.RESET;
	}
    public void onNextClicked(Button sender)
    {
        HidePanelName("winLevel");

        StateMgr.instance.State = GameState.BEGIN_LEVEL;
    }
}
