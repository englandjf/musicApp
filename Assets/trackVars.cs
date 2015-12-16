using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class trackVars : MonoBehaviour {


	InputField musicSpeed;
	public Text bpm;
	public int trackLength = 4;
	public int scaleNum;

	//Add screen dimensions
	public float topScreen,rightScreen,bottomScreen,leftScreen;
	public Camera subCamera;
	
	
	const int initValue = 1;//bps
	int currentValue;

	// Use this for initialization
	void Start () {
		//Get screen dimensions
		getScreenDim ();

		Canvas mainCanvas = GetComponentInChildren<Canvas> ();
		mainCanvas.worldCamera = subCamera;
		if (tag == "track") {
			musicSpeed = mainCanvas.GetComponentInChildren<InputField> ();
			musicSpeed.onValueChange.AddListener (delegate {
				updateValue ();
			});
			scaleNum = initValue;
			currentValue = initValue;	
			Text[] temp = mainCanvas.GetComponentsInChildren<Text> ();
			bpm = temp [1];
			//disable camera & canvas on creation
			GetComponentInChildren<Camera>().enabled = false;
			GetComponentInChildren<Canvas>().enabled = false;
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (tag == "track") {
			scaleNum = currentValue * 4;
			bpm.text = Mathf.Round (scaleNum * 15).ToString ();
		}
	}

	void updateValue()
	{
		if(tag == "track")
			currentValue = System.Int32.Parse (musicSpeed.text)/60;
	}

	//Used by all components of subtrack
	void getScreenDim()
	{
		subCamera = GetComponentInChildren<Camera> ();
		topScreen = subCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 10)).y;
		rightScreen = subCamera.ScreenToWorldPoint(new Vector3(Screen.width,0,10)).x;
		bottomScreen = subCamera.ScreenToWorldPoint(new Vector3(0,0,10)).y;
		leftScreen = subCamera.ScreenToWorldPoint(new Vector3(0,0,10)).x;
	}

}
