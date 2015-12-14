using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class trackVars : MonoBehaviour {


	InputField musicSpeed;
	//public float speedValue;
	public Text bpm;
	public int trackLength = 4;
	public int scaleNum;
	
	
	const int initValue = 1;//bps
	int currentValue;

	// Use this for initialization
	void Start () {
		Canvas mainCanvas = GetComponentInChildren<Canvas> ();
		mainCanvas.worldCamera = GetComponentInChildren<Camera> ();
		if (tag == "track") {
			musicSpeed = mainCanvas.GetComponentInChildren<InputField> ();
			musicSpeed.onValueChanged.AddListener (delegate {
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

}
