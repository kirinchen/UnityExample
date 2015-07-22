using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdShower : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Advertisement.Initialize ("51813");
		print ("JJJ");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showAd(){
		if(Advertisement.isReady()){ 
			Advertisement.Show(); 
			print("Show");
		}
		GameObject miss = GameObject.Find ("miss");
		Vector3 v3 = miss.transform.position;
		miss.transform.position = new Vector3 (v3.x,5f,v3.z);
	}
}
