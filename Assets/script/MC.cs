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
	void Start () {
		moveController = GetComponent<MoveController> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {	
        gameState = StateMgr.GetState();	
		if (gameState != GameState.PLAYING) {
			return;
		}
		float currDirX = moveController.normalizedMove.x;
        bool moving = moveController.normalizedMove.x != 0.0f;
		anim.SetBool ("onGround", onGround);
		if (disableControlOnAir) {
			moving = moving & onGround;
		}
		anim.SetBool ("moving", moving);

		bool flip = (currDirX * lastDirX) < 0.0f;
		if (flip) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
			lastDirX = currDirX;
		}
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
