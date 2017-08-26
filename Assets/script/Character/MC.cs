using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC : MonoBehaviour {

	public StateMgr stateMgr;
	private MoveController moveController;
	private SpriteRenderer spriteRenderer;
	private Animator anim;
	private GameState gameState;
    private Rigidbody2D rigid;
	void Start () {
		moveController = GetComponent<MoveController> ();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		anim = GetComponentInChildren<Animator> ();
        Debug.Log("Anim: " + LevelMgr.currentLevel);
        anim.SetInteger("level", LevelMgr.currentLevel);
	}
    private void FixedUpdate()
    {
        
    }
    void Update () {
//        gameState = StateMgr.instance.State;	
		if (stateMgr) {
			gameState = stateMgr.State;	
		} else {
			gameState = GameState.PLAYING;
		}
		if (gameState != GameState.PLAYING) {
			return;
		}
		float currDirX = moveController.currVel.x;
		if (currDirX != 0) {
//			spriteRenderer.flipX = Mathf.Sign(currDirX) < 0 ? true : false;
			transform.localScale = new Vector3(Mathf.Sign(currDirX), 1.0f, 1.0f);
		}     
	}
	void OnCollisionEnter2D (Collision2D col) {
		
			Debug.Log ("Collide " + col);

	}
}
