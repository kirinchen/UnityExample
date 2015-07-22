using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstBlockDownFinishEvent : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void onFinish(){		
		for(int i=0;i<transform.childCount;i++){
			GameObject go = transform.GetChild(i).gameObject;
			go.GetComponent<Block>().hide();
		}
		GetComponent<Animator> ().SetInteger ("speed",1);
		Player.getInstance ().level ++;
	}
}
