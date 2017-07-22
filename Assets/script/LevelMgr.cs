using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour {
	public Transform playerSpawner;
    public Transform levelGoal;
	public GameObject playerPref;
	public List<BaseCondition> levelConditions;

    private GameObject player;
    private GameState gameState;
	bool winLevel = false;
	// Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        UpdateLevelState(); 
	}

    void UpdateLevelState() {
		gameState = StateMgr.GetState();
        winLevel = true;
        switch(gameState) {
            case GameState.STARTING:
                {
                    StartingLevel();
                    break;
                }
            case GameState.RESET: 
                {
                    StateMgr.SetState(GameState.STARTING);
                    return;
                }            
            default: 
                {
					foreach (BaseCondition cd in levelConditions)
					{
						if (cd.completed == false)
						{
							winLevel = false;
							break;
						}
					}
					if (winLevel)
					{
						StateMgr.SetState(GameState.WON);
					}
                    break;
                }  
        }

    }
	public void StartingLevel()
	{
        if(player) {
            Destroy(player.gameObject);
        }
        player = (GameObject)Instantiate(playerPref, playerSpawner.position, playerSpawner.rotation);
		foreach (BaseCondition cd in levelConditions)
		{
			cd.completed = false;
		}
		Debug.Log("Player pos: " + player.transform.position);
        StateMgr.SetState(GameState.PLAYING);
	}
}
