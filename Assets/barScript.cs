using UnityEngine;
using System.Collections;

public class barScript : MonoBehaviour {

	Vector3 leftScreen,rightScreen;
	float midScreen;
	float distance;
    float screenDistance;
	trackVars tv;
	globalVars gv;
	public Camera ownerCam;
	public GameObject bar;
	bool play,stop;

	//may need to look into having two bars running if needed

	// Use this for initialization
	void Start () {
		midScreen = ownerCam.ScreenToWorldPoint (new Vector3 (0, Screen.height/2, 10)).y;
		leftScreen = ownerCam.ScreenToWorldPoint (new Vector3 (0, midScreen, 10));
		rightScreen = ownerCam.ScreenToWorldPoint (new Vector3 (Screen.width, midScreen, 10));


		distance = Vector3.Distance (leftScreen, rightScreen);
        screenDistance = distance;
		tv = GetComponent<trackVars> ();
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
		//sets starting position
		Vector3 temp = bar.transform.position;
		temp.x = leftScreen.x+.05f;
		bar.transform.position = temp;

		play = false;
		stop = false;

		//extend main track length, set default;
        /*
		if (this.gameObject.tag == "mainTrack") {
            Debug.Log(" " + gv.mainTrackLen);

            rightScreen.x += distance * (gv.mainTrackLen/ 4);
			distance += distance * (gv.mainTrackLen / 4);
            Debug.Log("DIs" + distance);
		}
        */

		//to avoid playing first not accidentally
		GetComponentInChildren<BoxCollider2D> ().enabled = false;


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (bar.transform.position.x <= rightScreen.x - .05f && play) {
			float temp;
            if (this.gameObject.tag == "mainTrack")
            {
                temp = (Time.fixedDeltaTime * screenDistance) / (4);//speed 
            }
            else
                temp = (Time.fixedDeltaTime * distance) / 4;//4 seconds
			bar.transform.Translate (temp, 0, 0);

		} else if (!play) {
			transform.position = transform.position;
		}
		else
		{
			Debug.Log(Time.time);
			if(this.gameObject.tag == "track")
				stop = true;
			else{
				Vector3 temp = new Vector3(leftScreen.x,midScreen,0);
				temp.x += .05f;
				bar.transform.position = temp;
			}
			/*
			if(this.gameObject.tag == "mainTrack"){
				Debug.Log(Time.time);
				Vector3 temp = new Vector3(leftScreen.x,midScreen,0);
				temp.x += .05f;
				bar.transform.position = temp;
			}
			*/
		}

		if (stop == true) {
			Vector3 temp = new Vector3(leftScreen.x,midScreen,0);
			temp.x += .05f;
			bar.transform.position = temp;
			GetComponentInChildren<BoxCollider2D> ().enabled = false;
		}

		//When track is changed
		if (!gv.currentTrack.Equals (this.gameObject) && overRidePlay == false && this.gameObject.tag != "mainTrack") {
			stop = true;
		}



	}

	bool overRidePlay = false;
	public void masterPlayClick()
	{
		overRidePlay = true;
		if (play) {
			Vector3 temp = new Vector3(leftScreen.x,midScreen,0);
			temp.x += .05f;
			bar.transform.position = temp;
			GetComponentInChildren<BoxCollider2D> ().enabled = false;
		}
		playClicked ();
	}

	public void playClicked()
	{
		//reset

		GetComponentInChildren<BoxCollider2D> ().enabled = true;
		play = true;
		stop = false;
	}

	public void pauseClicked()
	{
		play = false;
		stop = false;
	}

	public void stopClicked()
	{
		stop = true;
		play = false;
	}

    //changes the values of the track length
    //may need to change use of rightscreen
    public void trackLengthChanged()
    {
        rightScreen.x += distance*((gv.mainTrackLen-4)/4);
        distance += distance * ((gv.mainTrackLen-4)/4);
        Debug.Log("mt" + gv.mainTrackLen);
    }
}
