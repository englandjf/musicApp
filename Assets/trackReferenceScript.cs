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


    // Use this for initialization
    void Start () {
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
        getScreenInfo();
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
        if(a.gameObject.name == "bar" )
		    referenceTrack.GetComponent<barScript> ().masterPlayClick ();

	}

    float currentGrid = 1.0f;
    void handleSnap()
    {
        Debug.Log("s" + rightScreen);
        //X
        Vector3 snapPos = this.transform.position;
        float currentX = transform.position.x;
        float distanceFromLeft = Mathf.Abs(leftScreen - currentX);
        //there has to be a more efficient way...
        for (float a = leftScreen; a <= rightScreen; a += currentGrid)
        {
            if (currentX >= a && currentX < a + currentGrid)
            {
                snapPos.x = a + (currentGrid / 2);
                Debug.Log("ewas");
            }

        
        }
        //Y
        snapPos.y = Mathf.Round(snapPos.y);

        this.transform.position = snapPos;
    }

    void getScreenInfo()
    {
        Camera parentCam = GameObject.Find("mainTrack").GetComponentInChildren<Camera>();
        topScreen = parentCam.ScreenToWorldPoint(new Vector3(0, Screen.height, 10)).y;
        bottomScreen = parentCam.ScreenToWorldPoint(new Vector3(0, 0, 10)).y;
        leftScreen = parentCam.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
        rightScreen = parentCam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
    }
}
