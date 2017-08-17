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
                    ActiveOnlyPanel("IGM");
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
					ActiveOnlyPanel("Congratz");
					break;
				}
            case GameState.RESET: 
                {
                    HidePanelName("winLevel");
                    break;
                }
		}
    }
}
