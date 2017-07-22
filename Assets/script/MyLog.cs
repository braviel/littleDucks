using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLog : MonoBehaviour {
	public GUIStyle style;
	public bool accumulateLog;
	string myLog;

	Queue myLogQueue = new Queue();
	// Use this for initialization
	void Start () {
		Debug.Log ("Log debug");
	}
	void OnEnable () {
		Application.logMessageReceived += HandleLog;
	}

	void OnDisable () {
		Application.logMessageReceived -= HandleLog;
	}
	// Update is called once per frame
	void Update () {
		
	}

	void HandleLog (string logString, string stackTrace, LogType type) {
		myLog = logString;
		string newString = "\n [" + type + "] : " + myLog;
		if (accumulateLog) {
			myLogQueue.Enqueue (newString);
		}
		if (LogType.Exception == type) {
			newString = "\n" + stackTrace;
			myLogQueue.Enqueue (newString);
		}
		myLog = string.Empty;
		foreach (string mylog in myLogQueue) {
			myLog += mylog;
		}
	}

	void OnGUI() {
		GUILayout.Label (myLog, style);
	}
}
