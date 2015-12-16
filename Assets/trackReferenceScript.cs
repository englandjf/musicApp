using UnityEngine;
using System.Collections;

public class trackReferenceScript : MonoBehaviour {

	public GameObject referenceTrack;
	bool placed = false;
	bool mouseOver = false;
	globalVars gv;
    float topScreen;
    float bottomScreen;
    float leftScreen;
    float rightScreen;
	float distance;
	float currentScale;//set to the distance from the left side of the screen to the right because subtracks are only 4 seconds currently


    // Use this for initialization
    void Start () {
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
        getScreenInfo();
		distance = Mathf.Abs(leftScreen - rightScreen);
		currentScale = distance;

		this.transform.localScale = new Vector3 (currentScale, 1, 1);
		//this.transform.localPosition = new Vector3 (distance / 2, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) && placed == false) {
			if(placed == false){
				Vector3 temp = GameObject.Find("mainTrack").GetComponentInChildren<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
				transform.position = temp;
			}
		}
		if (Input.GetMouseButtonUp (0) && mouseOver) {
            handleSnap();
            placed = true;
			
		}
		
		if (Input.GetMouseButtonDown (1) && mouseOver) {
			Destroy(this.gameObject);
		}
		
		//Moves if already placed
		if (Input.GetMouseButton (0) && placed == true && mouseOver == true) {
			Vector3 temp = GameObject.Find("mainTrack").GetComponentInChildren<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
			transform.position = temp;
		}

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

	void OnTriggerEnter2D(Collider2D a)
	{
		Debug.Log (Time.time);
        if(a.gameObject.name == "bar" )
		    referenceTrack.GetComponent<barScript> ().masterPlayClick ();

	}

	const float secsPer = 4.0f;
	float currentGrid; 
	//snap by seconds
	//4 seconds per screen width
	//will need to grab the left side of the reference object and position it that way
    void handleSnap()
    {
		//only for testing will need to change
		const float hardCodedLeft = -21.24112f;

		currentGrid = distance / secsPer;
        Debug.Log("ls" + leftScreen);
        //X
        Vector3 snapPos = this.transform.position;
        float currentX = transform.position.x - (currentScale/2);//to get left side of object
        float distanceFromLeft = Mathf.Abs(hardCodedLeft - currentX);
        //there has to be a more efficient way...
        for (float a = hardCodedLeft; a <= rightScreen; a += currentGrid)
        {
            if (currentX >= a && currentX < a + currentGrid)
            {
                snapPos.x = a + (currentGrid+1);//- .5f because that is half the current scale
            }
        }
        //Y
        snapPos.y = Mathf.Round(snapPos.y);

        this.transform.position = snapPos;
    }

	//LEFT screen value changes when camera is moved
    void getScreenInfo()
    {
        Camera parentCam = GameObject.Find("mainTrack").GetComponentInChildren<Camera>();
        topScreen = parentCam.ScreenToWorldPoint(new Vector3(0, Screen.height, 10)).y;
        bottomScreen = parentCam.ScreenToWorldPoint(new Vector3(0, 0, 10)).y;
        leftScreen = parentCam.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
        rightScreen = parentCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
    }
}
