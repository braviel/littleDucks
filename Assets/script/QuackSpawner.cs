using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;
public class QuackSpawner : TileObject {
	public GameObject playerPref;
	private GameObject player;
	// Use this for initialization
	void Start () {
		SpawnObj ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SpawnObj() {
		if(player) {
			Destroy(player.gameObject);
		}
		player = (GameObject)Instantiate(playerPref, transform.position, transform.rotation);
	}

	void OnDestroy() {
		if(player) {
			Destroy(player.gameObject);
		}
	}
}
