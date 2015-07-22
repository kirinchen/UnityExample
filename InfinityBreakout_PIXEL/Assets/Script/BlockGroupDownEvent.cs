using UnityEngine;
using System.Collections;

public class BlockGroupDownEvent : MonoBehaviour {

	public void onBlockGroupDownStart(){
		for(int i=0;i<transform.childCount;i++){
			GameObject go = transform.GetChild(i).gameObject;
			go.GetComponent<Block>().revival();
		}
	}

	public void onlockGroupDownEnd(){
		Player.getInstance ().level ++;
	}
}
