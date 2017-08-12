using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPanel : Panel {

    public float timeout = 1.0f;

    private float begin;
	void Awake () {        
        begin = Time.realtimeSinceStartup;
        Debug.Log("Intro Awaked at " + begin);
        StartCoroutine(FadeOut());
	}
	
    IEnumerator FadeOut () {
        float currTime = Time.realtimeSinceStartup;
        float delta = (currTime - begin) / timeout;
        Debug.Log("Delta normalized: " + delta);
        yield return new WaitForSeconds(timeout);
        StateMgr.instance.State = GameState.LOBBY;
        Debug.Log("End!");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
