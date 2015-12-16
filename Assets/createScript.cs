using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class createScript : MonoBehaviour {


	bool overTone = false;
	trackVars tv;
	globalVars gv; //for selections
	float topScreen;

	Dropdown options;
	int currentTone = 1;

	// Use this for initialization
	void Start () {
		tv = GameObject.Find(this.transform.parent.name).GetComponent<trackVars> ();
		gv = GameObject.Find ("computer").GetComponent<globalVars> ();
		topScreen = GetComponent<Camera>().ScreenToWorldPoint (new Vector3(0, Screen.height, 0)).y;
		options = GameObject.Find(this.transform.parent.name).GetComponentInChildren<Canvas> ().GetComponentInChildren<Dropdown>() ;

		options.options = addData ();
		options.onValueChanged.AddListener(delegate {
			dropChanged();
		});
		Debug.Log (topScreen);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && gv.overObject == null && gv.currentTrack.Equals(this.transform.parent.gameObject)) {
			Vector3 temp = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
			if(temp.y <= topScreen-2)
			{
				Debug.Log(temp);
				//clean up
				GameObject a = new GameObject();
				if(currentTone == 1){
					a = (GameObject)Instantiate(tone1,temp,this.gameObject.transform.rotation);
				}
				else if(currentTone == 2){
					a = (GameObject)Instantiate(tone2,temp,this.gameObject.transform.rotation);
				}
				else if(currentTone == 3){
					a = (GameObject)Instantiate(tone3,temp,this.gameObject.transform.rotation);
				}
				else if(currentTone == 4){
					a = (GameObject)Instantiate(tone4,temp,this.gameObject.transform.rotation);
				}
				else if(currentTone == 5){
					a = (GameObject)Instantiate(tone5,temp,this.gameObject.transform.rotation);
				}
				a.GetComponent<hitScript>().parentName = this.transform.parent.gameObject.name;

			}
		}



	}

	 
	public GameObject tone1;
	public GameObject tone2;
	public GameObject tone3;
	public GameObject tone4;
	public GameObject tone5;
	const int toneAmt = 5;
	List<Dropdown.OptionData> addData()
	{
		List<Dropdown.OptionData> a = new List<Dropdown.OptionData> ();
		for (int i = 1; i <= toneAmt; i++) {
			Dropdown.OptionData b = new Dropdown.OptionData ();
			b.text = i.ToString();
			a.Add(b);
		}

		return a;
	}

	private void dropChanged()
	{
		currentTone = options.value+1;
	}



}
