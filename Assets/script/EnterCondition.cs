using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCondition : BaseCondition {

	void Start () {
		
	}

	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player") {
			this.completed = true;
		}
	}
}
