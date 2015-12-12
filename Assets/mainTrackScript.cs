using UnityEngine;
using System.Collections;

public class mainTrackScript : MonoBehaviour {

	Canvas mainCanvas;
	public GameObject beatTrack;
	public GameObject trackRef;
	globalVars gv;
	float topScreen;

	// Use this for initialization
	void Start () {
		mainCanvas = GetComponentInChildren<Canvas> ();
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
		topScreen = GetComponentInChildren<Camera>().ScreenToWorldPoint (new Vector3(0, Screen.height, 0)).y;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("q")) {
			Vector3 temp = GetComponentInChildren<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
			if(temp.y <= topScreen -2){
				GameObject refOb = (GameObject)Instantiate(trackRef,temp,this.transform.rotation);
				refOb.GetComponent<trackReferenceScript>().referenceTrack = gv.getIndex(1);
			}
			//Set which object it is referring to

		}

	}

	public void addTrack()
	{
		gv.trackAdded (Instantiate (beatTrack, gv.nextTrackPos, this.transform.rotation));

	}


}
