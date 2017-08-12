using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC : MonoBehaviour {

	public bool disableControlOnAir = true;
	private bool onGround = false;
	private MoveController moveController;
	private SpriteRenderer spriteRenderer;
	private Animator anim;
	private float lastDirX = 1.0f;
	private GameState gameState;
    private Rigidbody2D rigid;
	void Start () {
		moveController = GetComponent<MoveController> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
        rigid = GetComponent<Rigidbody2D> ();
        Debug.Log("Anim: " + LevelMgr.currentLevel);
        anim.SetInteger("level", LevelMgr.currentLevel);
	}
    private void FixedUpdate()
    {
        
    }
    void Update () {
        gameState = StateMgr.instance.State;	
		if (gameState != GameState.PLAYING) {
			return;
		}
		float currDirX = moveController.movingIntensitive.x;
        bool moving = moveController.movingIntensitive.x != 0.0f;
        //if (anim)
        //{
        //    anim.SetBool("onGround", onGround);
        //    if (disableControlOnAir)
        //    {
        //        moving = moving & onGround;
        //    }
        //    anim.SetBool("moving", moving);
        //} else {
        //    Debug.Log("No anim!!");
        //}
        //
		bool flip = (currDirX * lastDirX) < 0.0f;
		if (flip) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
			lastDirX = currDirX;
		}
        ////
        //if (moveController.enabled) {
        //    transform.position += new Vector3(moveController.nextPos.x, 0.0f, 0.0f);
        //    //rigid.MovePosition(new Vector2(transform.position.x + moveController.finalMove.x, transform.position.y));
        //} else {
        //    Debug.Log("");
        //    //rigid.MovePosition(new Vector2(transform.position.x, transform.position.y - 3.0f));
        //    //transform.position += new Vector3(0.0f, -0.0f, 0.0f);
        //}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "ground") {
			onGround = true;
			if (moveController.enabled == false) {
				moveController.enabled = true;
			}
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "ground") {
			onGround = false;
			if (disableControlOnAir) {
				moveController.enabled = false;
			}
		}
	}
}
