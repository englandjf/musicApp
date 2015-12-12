using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class mainTrackScript : MonoBehaviour {

	Canvas mainCanvas;
	public GameObject beatTrack;
	public GameObject trackRef;
	globalVars gv;
	float topScreen;
    Camera mainCamera;
    Dropdown subTracks;

	// Use this for initialization
	void Start () {
		mainCanvas = GetComponentInChildren<Canvas> ();
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
        mainCamera = GetComponentInChildren<Camera>();
        topScreen = mainCamera.ScreenToWorldPoint (new Vector3(0, Screen.height, 0)).y;
        subTracks = mainCanvas.GetComponentInChildren<Dropdown>();
        //clear dropdown
        List<Dropdown.OptionData> a = new List<Dropdown.OptionData>();
        subTracks.options = a;
        subTracks.onValueChanged.AddListener(delegate {
            dropChanged();
        });

    }

    /*
    Mobile notes
    -one finger for creation and selection
    -two fingers for movement
    */
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("q")) {
			Vector3 temp = GetComponentInChildren<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
			if(temp.y <= topScreen -2){
				GameObject refOb = (GameObject)Instantiate(trackRef,temp,this.transform.rotation);
                Debug.Log(subTracks.value);
				refOb.GetComponent<trackReferenceScript>().referenceTrack = gv.getIndex(subTracks.value+1);
			}
			//Set which object it is referring to

		}

        if (Input.GetKey(KeyCode.LeftArrow)) {
            Vector3 temp = mainCamera.transform.position;
            temp.x -= .1f;
            mainCamera.transform.position = temp;
         }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 temp = mainCamera.transform.position;
            temp.x += .1f;
            mainCamera.transform.position = temp;
        }

    }

	public void addTrack()
	{
		gv.trackAdded (Instantiate (beatTrack, gv.nextTrackPos, this.transform.rotation));
        Dropdown.OptionData b = new Dropdown.OptionData();
        b.text = (gv.nextTrackIndex - 1).ToString();
        subTracks.options.Add(b);
        subTracks.value = gv.nextTrackIndex - 1;

	}

    void dropChanged()
    {
    }


}
