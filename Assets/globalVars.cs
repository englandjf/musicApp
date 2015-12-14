using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class globalVars : MonoBehaviour {
	
	public Vector3 nextTrackPos;
	public int nextTrackIndex = 1;
	GameObject[] allTracks;
	public GameObject overObject;
	public GameObject currentTrack;
	int currentTrackIndex = 0;
    public int mainTrackLen;

	// Use this for initialization
	void Start () {
		allTracks = new GameObject[100];
		allTracks[0] = GameObject.Find("mainTrack");

		//Camera.SetupCurrent (allTracks [0].GetComponentInChildren<Camera> ());
		//disableCameras ();
		allTracks[0].GetComponentInChildren<Camera> ().enabled = true;
		allTracks[0].GetComponentInChildren<Canvas> ().enabled = true;
		currentTrack = allTracks[0];
		nextTrackPos = new Vector3 (0, 10, -10);
		Debug.Log ("zz " + currentTrack.name);
        mainTrackLen = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)){
			//disable current camera
			if(currentTrackIndex + 1 < nextTrackIndex){
				currentTrack.GetComponentInChildren<Camera> ().enabled = false;
				currentTrack.GetComponentInChildren<Canvas> ().enabled = false;
				currentTrackIndex++;
				currentTrack = allTracks [currentTrackIndex];
				currentTrack.GetComponentInChildren<Camera> ().enabled = true;
				currentTrack.GetComponentInChildren<Canvas> ().enabled = true;
			}



		}
		else if (Input.GetKeyDown (KeyCode.DownArrow)){
			if(currentTrackIndex - 1 >= 0){
				currentTrack.GetComponentInChildren<Camera> ().enabled = false;
				currentTrack.GetComponentInChildren<Canvas> ().enabled = false;
				currentTrackIndex--;
				currentTrack = allTracks [currentTrackIndex];
				currentTrack.GetComponentInChildren<Camera> ().enabled = true;
				currentTrack.GetComponentInChildren<Canvas> ().enabled = true;
			}



		}
	}

	public void trackAdded(Object track)
	{
		GameObject temp = (GameObject)track;
		temp.name = "subTrack" + nextTrackIndex;
		nextTrackPos.y += 10;
		allTracks [nextTrackIndex] = temp;
		nextTrackIndex++;
		Debug.Log (allTracks.Length);
	}

	//might just be for testing
	void disableCameras()
	{
		for (int i =0; i < allTracks.Length; i++) {
			allTracks [i].GetComponentInChildren<Camera> ().enabled = false;
			allTracks [i].GetComponentInChildren<Canvas> ().enabled = false;
		}
	}

	public GameObject getIndex(int a)
	{
		return allTracks[a];
	}

	public void stopAll()
	{
		for (int i = 1; i < this.nextTrackIndex; i++)
			allTracks [i].GetComponent<barScript> ().stopClicked ();
	}


}
