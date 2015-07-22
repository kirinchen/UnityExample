using UnityEngine;
using System.Collections;

public class CreatOtherBlockGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void creat(){
		GameObject[] gos =GameObject.FindGameObjectsWithTag ("BlockGroup");
		if(gos.Length == 2){
			createOtherOne();
		}
	}

	private void createOtherOne(){
		GameObject ctrl = GameObject.Find ("GameManager");
		BlockCreator bc = ctrl.GetComponent<BlockCreator>();
		bc.create ();
	} 
}
