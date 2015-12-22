using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class mainTrackScript : MonoBehaviour {

	Canvas mainCanvas;
	public GameObject beatTrack;
	public GameObject trackRef;
	public GameObject editRef;
	globalVars gv;
	float topScreen;
    Camera mainCamera;
    Dropdown menuOptions;
    InputField trackLength;

	//Will need to look into options for having sounds/beats in the cloud
	public AudioClip[] extraSounds;

	// Use this for initialization
	void Start () {
		mainCanvas = GetComponentInChildren<Canvas> ();
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
        mainCamera = GetComponentInChildren<Camera>();
        topScreen = mainCamera.ScreenToWorldPoint (new Vector3(0, Screen.height, 0)).y;
        menuOptions = mainCanvas.GetComponentInChildren<Dropdown>();
        trackLength = mainCanvas.GetComponentInChildren<InputField>();

        trackLength.onValueChange.AddListener(delegate
        {
            lengthChange();
        });


		setupMenu ();

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
				if(inMenu == currentMenu.sub){
					refOb.GetComponent<trackReferenceScript>().referenceTrack = gv.getIndex(menuOptions.value); 
				}
				else if(inMenu == currentMenu.sound)
				{
					refOb.GetComponent<trackReferenceScript>().referenceClip = extraSounds[menuOptions.value-1];
				}
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
		//Sub track
		GameObject tempSub = (GameObject)Instantiate (beatTrack, gv.nextTrackPos, this.transform.rotation);
		gv.trackAdded (tempSub);
        Dropdown.OptionData b = new Dropdown.OptionData();
        b.text = (gv.nextTrackIndex - 1).ToString();
        subTracks.Add(b);
        menuOptions.options = subTracks;
		inMenu = currentMenu.sub;
        menuOptions.value = gv.nextTrackIndex - 1;


		//Edit Menu
		GameObject tempEdit = (GameObject)Instantiate (editRef, new Vector3(tempSub.transform.position.x-50,tempSub.transform.position.y,tempSub.transform.position.z), this.transform.rotation);
		tempEdit.GetComponent<editSoundScript> ().subRef = tempSub;
		tempEdit.name = tempSub.name + "edit";
		//Disable camera & canvas
		tempSub.GetComponent<trackVars> ().editSound = tempEdit;
		tempEdit.GetComponentInChildren<Camera> ().enabled = false;
		tempEdit.GetComponentInChildren<Canvas> ().enabled = false;

		//set sound ref as well, probably on double click
	}

    //first layer of menu
    List<Dropdown.OptionData> mainMenu;
    //second layer of menu
    List<Dropdown.OptionData> subTracks;
    //True if in second layer
	List<Dropdown.OptionData> soundMenu;
	//Enums for menu navigation
	enum currentMenu{main,sub,sound};
	currentMenu inMenu;
    void dropChanged()
    {
		//Menus->Sub Menus
		if(menuOptions.value == 0)
        {
            //into submenu only if a track has been added
            if (inMenu != currentMenu.sub && gv.nextTrackIndex > 1)
            {
                menuOptions.options = subTracks;
				inMenu = currentMenu.sub;
            }
			//Go back
            else {
                menuOptions.options = mainMenu;
				inMenu = currentMenu.main;
            }
        }
        //second option, other sound effects
        else if(menuOptions.value == 1)
        {
			//into submenu only if a track has been added
			if (inMenu == currentMenu.main)
			{
				menuOptions.options = soundMenu;
				inMenu = currentMenu.sound;
			}
        }


    }

	void setupMenu()
	{
		//Main menu
		mainMenu = new List<Dropdown.OptionData>();
		//Add menu options
		mainMenu.Add (new Dropdown.OptionData("Sub Tracks"));
		mainMenu.Add (new Dropdown.OptionData("Sounds"));
		menuOptions.options = mainMenu;
		menuOptions.value = 1;
		menuOptions.onValueChanged.AddListener(delegate {
			dropChanged();
		});
		inMenu = currentMenu.main;

		//Sub track menu
		subTracks = new List<Dropdown.OptionData> ();
		subTracks.Add(new Dropdown.OptionData("Back"));

		//Sound menu
		soundMenu = new List<Dropdown.OptionData> ();
		soundMenu.Add(new Dropdown.OptionData("Back"));
		for (int i = 1; i <= extraSounds.Length; i++) {
			soundMenu.Add(new Dropdown.OptionData(i.ToString()));
		}

	}
	


}
