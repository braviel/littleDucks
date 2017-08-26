using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameState {
	INTRO,
    LOBBY,
    LEVEL_CHOSE,
	TOP_SCORE,
    BEGIN_LEVEL,
	PLAYING,
	PAUSING,
	WON_LEVEL,
    WON_GAME,
	STOP,
	RESET
}
public class StateMgr : MonoBehaviour {
	
//    public static StateMgr instance = null;
    public MenuMgr menuMgr;
    public LevelMgr levelMgr;

    public GameState beginState = GameState.INTRO;
    private GameState _state;
    public GameState State { 
        get {
            return _state;
        }
        set {
            _state = value;
            menuMgr.OnChageState();
            levelMgr.OnChangeState();
        }
    }
    void Awake()
    {
//        if(instance == null) {
//            instance = this;
//        } else if(instance != this) {
//            Destroy(gameObject);  
//        }
        DontDestroyOnLoad(gameObject);
        Init();
    }
    void Init () {
        Debug.Log("StateMgr init with State LOBBY");
        this.State = beginState;		
	}
    public void NextLevel() {
        levelMgr.NextLevel();
        this.State = GameState.BEGIN_LEVEL;
    }
    public void GoToLevel(int level) {
        levelMgr.GoToLevel(level);
		this.State = GameState.BEGIN_LEVEL;
    }
	void Update () {
	    
	}
	
}
