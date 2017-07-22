using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {
	public Vector2 maxSpeed = new Vector2(3.0f, 3.0f);
	public Vector2 currSpeed = Vector2.zero;
	public float accel = 30.0f;
	public Vector2 normalizedMove;
	private GameState gameState;
	void Start () {
		
	}

	void Update () {		
        gameState = StateMgr.GetState();	
		if (gameState != GameState.PLAYING) {
			return;
		}
		normalizedMove = GetInputAxis ();
		if (normalizedMove != Vector2.zero) {
//			Debug.Log ("NOrmalized Move: " + normalizedMove);
			currSpeed = Vector2.Lerp (currSpeed, maxSpeed, Time.deltaTime * accel);
		} else {
			currSpeed = Vector2.Lerp (currSpeed, Vector2.zero, Time.deltaTime * accel);
		}
		transform.position += new Vector3 (currSpeed.x * normalizedMove.x, 0.0f, 0.0f);
	}

	Vector2 GetInputAxis () {
	#if UNITY_EDITOR
		return new Vector2 (
			Input.GetAxis("Horizontal"), 
			Input.GetAxis("Vertical")
		);
	#else
		return new Vector2 (
			Input.acceleration.x,
			Input.acceleration.y
		);
	#endif
	}
}
