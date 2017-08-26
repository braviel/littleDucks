using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Congratz : Panel {
	// Use this for initialization
	void Start () {
        //Image i = GetComponent<Image>();
        //movie = (MovieTexture)i.mainTexture;
        //if(movie.isPlaying) {
        //    movie.Pause();
        //} else {
        //    movie.Play();
        //}
	}	
	// Update is called once per frame
	void Update () {
		
	}

	public void onRetryClicked(Button sender)
	{
		stateMgr.State = GameState.RESET;
	}
	public void onNextClicked(Button sender)
	{
		stateMgr.NextLevel();
	}
}
