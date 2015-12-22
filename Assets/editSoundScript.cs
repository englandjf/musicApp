using UnityEngine;
using System.Collections;

public class editSoundScript : MonoBehaviour {

	//Reference to the subtrack
	public GameObject subRef;
	//Reference to the sound itself
	public GameObject soundRef;
	globalVars gv;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//When a double click occurs on the sound, set canvas and camera to false on the track
	//Set canvas and camera enabled on the setttings page
	//May need different menus for trackRef and beats, beats first

	public void backTotrack()
	{
		GetComponentInChildren<Camera> ().enabled = false;
		GetComponentInChildren<Canvas> ().enabled = false;
		Debug.Log ("Sub " + subRef.name);
		subRef.GetComponentInChildren<Camera> ().enabled = true;
		subRef.GetComponentInChildren<Canvas> ().enabled = true;
		subRef.GetComponent<trackVars> ().editing = false;
	}

	//Add options for enabling echo, chorus, etc

}
