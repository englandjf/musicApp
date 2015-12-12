using UnityEngine;
using System.Collections;

public class trackReferenceScript : MonoBehaviour {

	public GameObject referenceTrack;
	bool placed = false;
	bool mouseOver = false;
	globalVars gv;

		
	// Use this for initialization
	void Start () {
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
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
		referenceTrack.GetComponent<barScript> ().masterPlayClick ();

	}
}
