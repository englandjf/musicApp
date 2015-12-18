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
    Dropdown menuOptions;
    InputField trackLength; 

	// Use this for initialization
	void Start () {
		mainCanvas = GetComponentInChildren<Canvas> ();
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
        mainCamera = GetComponentInChildren<Camera>();
        topScreen = mainCamera.ScreenToWorldPoint (new Vector3(0, Screen.height, 0)).y;
        menuOptions = mainCanvas.GetComponentInChildren<Dropdown>();
        trackLength = mainCanvas.GetComponentInChildren<InputField>();
        //clear dropdown
        menuOptions.options = new List<Dropdown.OptionData>();
		//Ad sub options
		Dropdown.OptionData st = new Dropdown.OptionData ("Sub Tracks");
		menuOptions.options.Add (st);
		Dropdown.OptionData ss = new Dropdown.OptionData ("Sounds");
		menuOptions.options.Add (ss);
		menuOptions.value = 1;
        //subTracks.options = a;
        menuOptions.onValueChanged.AddListener(delegate {
            dropChanged();
        });

        trackLength.onValueChange.AddListener(delegate
        {
            lengthChange();
        });

		subTracks = new List<Dropdown.OptionData> ();
		//subTracks.onValueChanged.AddListener(delegate {
		//	dropChanged();
		//});

    }
	
    void lengthChange()
    {
        gv.mainTrackLen = System.Int32.Parse(trackLength.text);
        GetComponent<barScript>().trackLengthChanged();
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
                Debug.Log(menuOptions.value);
				refOb.GetComponent<trackReferenceScript>().referenceTrack = gv.getIndex(menuOptions.value+1); 
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
        subTracks.Add(b);
        menuOptions.value = gv.nextTrackIndex - 1;

	}

	List<Dropdown.OptionData> subTracks;
    void dropChanged()
    {
		//Menus->Sub Menus
		//if(menuOptions.value == 0)

    }


}
