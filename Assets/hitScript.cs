using UnityEngine;
using System.Collections;

public class hitScript : MonoBehaviour {

	//piano https://www.freesound.org/people/jobro/packs/2489/?page=1#sound

	//Effect that will be played when object is hit
	AudioSource noise;
	//True if object has been placed after initial creation
	bool placed = false;
	//True if mouse is currently over object
	bool mouseOver = false;

	trackVars tv;
	globalVars gv; //for selection

	//might change
	public string parentName;

	

	// Use this for initialization
	void Start () {

		tv = GameObject.Find (parentName).GetComponent<trackVars> ();
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
		noise = gameObject.GetComponentInChildren<AudioSource> ();
		changeScale ();

	}
	
	// Update is called once per frame
	void Update () {
		//Initial placement after creation
		if (Input.GetMouseButton(0) && placed == false) {
			if(placed == false){
				Vector3 temp = tv.subCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
				if(temp.y < tv.topScreen -2)
					transform.position = temp;
			}
		}
		//Moving after object has already been placed
		if (Input.GetMouseButtonUp (0) && mouseOver) {
			handleSnap();
			//Set pitch based on y
			noise.pitch = 1;
			noise.pitch += (transform.position.y-tv.bottomScreen) * .1f;//use bottom screen y for balance
			Debug.Log(transform.position);
			placed = true;

		}
		//Right click will delete object
		if (Input.GetMouseButtonDown (1) && mouseOver) {
			Destroy(this.gameObject);
		}

		//Moves if already placed
		if (Input.GetMouseButton (0) && placed == true && mouseOver == true) {
			Vector3 temp = tv.subCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
			if(temp.y < tv.topScreen -2)
				transform.position = temp;
		}
	}

	//When object is hit by the bar
	void OnTriggerEnter2D(Collider2D a)
	{
			noise.Play ();
	}



	float blockScaleNum;
	//Camera parentCam;
	float distance;
	void changeScale()
	{
		distance = Mathf.Abs(tv.leftScreen - tv.rightScreen);
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
		float distanceFromLeft = Mathf.Abs (tv.leftScreen - currentX);
		//there has to be a more efficient way...
		for (float a = tv.leftScreen; a <= tv.rightScreen; a+=blockScaleNum) {
			if(currentX >= a && currentX < a+blockScaleNum)
				snapPos.x = a+(blockScaleNum/2);
		}
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
	
}
