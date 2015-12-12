using UnityEngine;
using System.Collections;

public class hitScript : MonoBehaviour {

	AudioSource noise;
	bool placed = false;
	bool mouseOver = false;
	bool overTone = false;
	trackVars tv;
	globalVars gv; //for selection
	public string parentName;
	float topScreen;
	float bottomScreen;
	float leftScreen;
	float rightScreen;

	// Use this for initialization
	void Start () {

		tv = GameObject.Find (parentName).GetComponent<trackVars> ();
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
		parentCam = GameObject.Find (parentName).GetComponentInChildren<Camera> ();
		noise = gameObject.GetComponentInChildren<AudioSource> ();
		getScreenInfo ();
		changeScale ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) && placed == false) {
			if(placed == false){
				Vector3 temp = parentCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
				if(temp.y < topScreen -2)
					transform.position = temp;
			}
		}
		if (Input.GetMouseButtonUp (0) && mouseOver) {
			handleSnap();
			//Set pitch based on y
			noise.pitch = 1;
			noise.pitch += (transform.position.y-bottomScreen) * .1f;//use bottom screen y for balance
			Debug.Log(transform.position);
			placed = true;

		}

		if (Input.GetMouseButtonDown (1) && mouseOver) {
			Destroy(this.gameObject);
		}

		//Moves if already placed
		if (Input.GetMouseButton (0) && placed == true && mouseOver == true) {
			Vector3 temp = parentCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
			if(temp.y < topScreen -2)
				transform.position = temp;
		}
	}

	void OnTriggerEnter2D(Collider2D a)
	{
		//if (!this.noise.isPlaying)
			noise.Play ();

	}

	float blockScaleNum;
	Camera parentCam;
	float distance;
	void changeScale()
	{
		//float leftScreen = parentCam.ScreenToWorldPoint (new Vector3 (0, 0, 10)).x;
		//float rightScreen = parentCam.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 10)).x;
		distance = Mathf.Abs(leftScreen - rightScreen);
		Debug.Log ("Distance " + distance);
		blockScaleNum = distance/tv.scaleNum;
		Debug.Log ("SCALE " + blockScaleNum);
		Vector3 temp = transform.localScale;
		temp.x = blockScaleNum;

		transform.localScale = temp;

	}

	void handleSnap()
	{

		//X
		Vector3 snapPos = this.transform.position;
		float currentX = transform.position.x;
		float distanceFromLeft = Mathf.Abs (leftScreen - currentX);
		//there has to be a more efficient way...
		for (float a = leftScreen; a <= rightScreen; a+=blockScaleNum) {
			if(currentX >= a && currentX < a+blockScaleNum)
				snapPos.x = a+(blockScaleNum/2);
		}



		/*
		Vector3 snapPos = transform.position;
		float xAmt = snapPos.x;
		xAmt /= scaleNum;
		xAmt = Mathf.Round (xAmt);
		Debug.Log ("X " + xAmt);
		xAmt *= scaleNum;
		snapPos.x = xAmt+(scaleNum/4);
	*/
		//Y
		snapPos.y = Mathf.Round (snapPos.y);

		this.transform.position = snapPos;
	}


	void OnMouseOver()
	{
		mouseOver = true;
		gv.overObject = this.gameObject;
	}

	void OnMouseExit()
	{
		mouseOver = false;
		gv.overObject = null;
	}

	//possibly move to trackvars
	void getScreenInfo()
	{
		topScreen = parentCam.ScreenToWorldPoint (new Vector3 (0, Screen.height, 10)).y;
		bottomScreen = parentCam.ScreenToWorldPoint (new Vector3 (0,0, 10)).y;
		leftScreen = parentCam.ScreenToWorldPoint (new Vector3 (0, 0, 10)).x;
		rightScreen = parentCam.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 10)).x;
	}
}
