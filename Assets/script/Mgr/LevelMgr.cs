using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMgr : MonoBehaviour {
	public StateMgr stateMgr;
	public QuackSpawner spawnerPrefab;
//	public Transform playerSpawner;
    public Transform levelGoal;
//	public GameObject playerPref;
    public List<GameObject> lvlPrefabs;
	public List<BaseCondition> levelConditions;
    public static int currentLevel = 0;
//    private GameObject player;
	private QuackSpawner spawner;
    private GameObject level;
    private GameState gameState;
	bool winLevel = false;
	// Use this for initialization
    void Awake () {
        //Debug.Log("State " + gameState);
        
    }
	void Init() {
		levelGoal.gameObject.SetActive(false);
	}
	// Update is called once per frame
	void Update () {
		if (stateMgr && stateMgr.State != GameState.WON_LEVEL)
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
				stateMgr.State = GameState.WON_LEVEL;
            }
        }
	}
    public void OnChangeState() {
		gameState = stateMgr.State;
        switch(gameState) {
            case GameState.BEGIN_LEVEL:
                {
                    StartingLevel();
                    break;
                }
            case GameState.RESET: 
                {
					stateMgr.State = GameState.BEGIN_LEVEL;
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
		//		spawnerPrefab
		if(spawner) {
			Debug.Log ("Destroy old spawner");
			Destroy(spawner.gameObject);
		}
		foreach(Transform t in level.transform) {
//			Debug.Log ("Traverse object: [ " + t.gameObject.name + " ]");
			if (t.gameObject.name == "obj") {
				foreach (Transform g in t) {
					if (g.gameObject.tag == "spawner") {
						spawner = Instantiate (spawnerPrefab, g.position, g.rotation) as QuackSpawner;
					}
				}
			}

		}
//		Debug.Log("Player pos: " + player.transform.position);
		stateMgr.State = GameState.PLAYING;
        levelGoal.gameObject.SetActive(true);
	}


}
