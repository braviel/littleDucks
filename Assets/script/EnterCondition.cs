using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class EnterCondition : BaseCondition {

	void Start () {
		
	}

	void Update () {
		Debug.Log ("Mother update");
	}
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Trigger Called");
			this.completed = true;
		}
	}
	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Collision Called");
			this.completed = true;
		}
	}
}
