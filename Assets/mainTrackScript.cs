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
        mainMenu = new List<Dropdown.OptionData>();
		//Add menu options
	    mainMenu.Add (new Dropdown.OptionData("Sub Tracks"));
		mainMenu.Add (new Dropdown.OptionData("Sounds"));
        menuOptions.options = mainMenu;
		menuOptions.value = 1;

        menuOptions.onValueChanged.AddListener(delegate {
            dropChanged();
        });

        trackLength.onValueChange.AddListener(delegate
        {
            lengthChange();
        });

		subTracks = new List<Dropdown.OptionData> ();
        //will need to add later
        subTracks.Add(new Dropdown.OptionData("Back"));

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
				refOb.GetComponent<trackReferenceScript>().referenceTrack = gv.getIndex(menuOptions.value); 
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
        menuOptions.options = subTracks;
        inSub = true;
        menuOptions.value = gv.nextTrackIndex - 1;


	}

    //first layer of menu
    List<Dropdown.OptionData> mainMenu;
    //second layer of menu
    List<Dropdown.OptionData> subTracks;
    //True if in second layer
    bool inSub = false;
    void dropChanged()
    {
		//Menus->Sub Menus
		if(menuOptions.value == 0)
        {
            //into submenu only if a track has been added
            if (!inSub && gv.nextTrackIndex > 1)
            {
                menuOptions.options = subTracks;
                inSub = true;
            }
            else {
                menuOptions.options = mainMenu;
                inSub = false;
            }
        }
        //second option, other sound effects
        else if(menuOptions.value == 1)
        {

        }


    }


}
