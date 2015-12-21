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
	//Track length should be in beats
	//beat size * beats = track length

	// Use this for initialization
	void Start () {
		midScreen = ownerCam.ScreenToWorldPoint (new Vector3 (0, Screen.height/2, 10)).y;
		leftScreen = ownerCam.ScreenToWorldPoint (new Vector3 (0, midScreen, 10));
		rightScreen = ownerCam.ScreenToWorldPoint (new Vector3 (Screen.width, midScreen, 10));


		distance = Vector3.Distance (leftScreen, rightScreen);
        screenDistance = distance;//for constant speed
		tv = GetComponent<trackVars> ();
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
		//sets starting position
		Vector3 temp = bar.transform.position;
		temp.x = leftScreen.x+.05f;
		bar.transform.position = temp;

		play = false;
		stop = false;

		//to avoid playing first not accidentally
		GetComponentInChildren<BoxCollider2D> ().enabled = false;

		drawSecs ();

		//Set special features for subtracks
		if(this.gameObject.tag == "track")
			subTrackSpecial ();
	}

	//time from left to right
	const int lrSeconds = 10;
	// Update is called once per frame
	void FixedUpdate () {
		if (bar.transform.position.x <= rightScreen.x - .05f && play) {
			float temp;
            if (this.gameObject.tag == "mainTrack")
            {
				temp = (Time.fixedDeltaTime * screenDistance) / lrSeconds;
            }
            else
                temp = (Time.fixedDeltaTime * screenDistance) / 4;//4 seconds
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
	//currently on works with divisibles of 10?
    public void trackLengthChanged()
    {
        rightScreen.x += distance*((gv.mainTrackLen-lrSeconds)/lrSeconds);
        distance += distance * ((gv.mainTrackLen-lrSeconds)/lrSeconds);
        Debug.Log("mt" + gv.mainTrackLen);
    }

	public GameObject secs;
	void drawSecs()
	{
		if (this.gameObject.tag != "track") {
			float bottomScreen = ownerCam.ScreenToWorldPoint (new Vector3 (0, 0, 10)).y;
			float temp = distance / lrSeconds;
			Debug.Log("bar " + temp);
			for (int i = 0; i <30; i++)
				Instantiate (secs, new Vector3 (leftScreen.x + (temp * i), bottomScreen + .5F, 10), this.transform.rotation);//curently hardcoded left screen
		}
	}

	float blockScaleNum;
	void subTrackSpecial()
	{

		//set right screen/ subtrack length/ beat amount
		//left screen = beat size * beats
		//float bps = tv.bpm / 60;
		//blockScaleNum = screenDistance / tv.bpm
		//leftScreen.x += blockScaleNum * 4;
		//Debug.Log("LEft " + leftScreen.x + " Right " + rightScreen.x);
		//rightScreen.x = leftScreen.x + (tv.currentScale * 4);
		//Debug.Log("LEft " + leftScreen.x + " Right " + tv.currentScale * 4);
	}

	int beatsToPlay = 4;//how many beats to play
	public void changeSubSettings()
	{
		float blockScaleNum = screenDistance/tv.scaleNum;
		rightScreen.x = leftScreen.x + (blockScaleNum * beatsToPlay);
		Debug.Log ("RS " + rightScreen.x);
	}

	public void moreBeats()
	{
		beatsToPlay++;
		changeSubSettings ();
	}

	public void lessBeats()
	{
		if (beatsToPlay - 1 > 0) {
			beatsToPlay--;
			changeSubSettings ();
		}
	}
}
