using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour {
	public Transform playerSpawner;
    public Transform levelGoal;
	public GameObject playerPref;
    public List<GameObject> lvlPrefabs;
	public List<BaseCondition> levelConditions;
    public static int currentLevel = 0;
    private GameObject player;
    private GameObject level;
    private GameState gameState;
	bool winLevel = false;
	// Use this for initialization
    void Awake () {
        //Debug.Log("State " + gameState);
        levelGoal.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (StateMgr.instance.State != GameState.WON_LEVEL)
        {
            winLevel = true;
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
                Debug.Log("Set Win");
                StateMgr.instance.State = GameState.WON_LEVEL;
            }
        }
	}
    public void OnChangeState() {
        gameState = StateMgr.instance.State;
        switch(gameState) {
            case GameState.BEGIN_LEVEL:
                {
                    StartingLevel();
                    break;
                }
            case GameState.RESET: 
                {
                    StateMgr.instance.State = GameState.BEGIN_LEVEL;
                    return;
                }
            case GameState.LEVEL_CHOSE:
				{
                    //StateMgr.instance.State = GameState.LEVEL_CHOSE;
					return;
				}
            case GameState.WON_LEVEL: 
                {
                    
                    return;    
                }
			//case GameState.NEXTSTAGE:
			//{
			//    if(lvlPrefabs.Count != 0)
			//    currentLevel = (currentLevel + 1) % lvlPrefabs.Count;
			//    StateMgr.instance.State = GameState.STARTING;
			//    return;
			//}
			default: 
                {
                    return;
                }  
        }

    }

    public void NextLevel() {
        if (lvlPrefabs.Count != 0) {
            currentLevel = (currentLevel + 1) % lvlPrefabs.Count;
        }
	}
	public void GoToLevel(int level)
	{
		if (lvlPrefabs.Count != 0)
		{
            Debug.Log("Go to level :" + level);
            currentLevel = level % lvlPrefabs.Count;
		}
	}
	public void StartingLevel()
	{
        if(player) {
            Destroy(player.gameObject);
        }
        player = (GameObject)Instantiate(playerPref, playerSpawner.position, playerSpawner.rotation);
        if(level) {
            Destroy(level);
        }
        Debug.Log("Starting level: " + currentLevel);
        level = Instantiate(lvlPrefabs[currentLevel], transform.position, transform.rotation);
        level.transform.parent = transform;
		foreach (BaseCondition cd in levelConditions)
		{
			cd.completed = false;
		}
		Debug.Log("Player pos: " + player.transform.position);
        StateMgr.instance.State = GameState.PLAYING;
        levelGoal.gameObject.SetActive(true);
	}


}
