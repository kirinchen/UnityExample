using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalMethod  {

	public static bool dice(float f){
		int idx = Random.Range(0,1000);
		int baseNum = (int)(1000 * f);
		if (idx < baseNum) {
			return true;
		} else {
			return false;
		}
	}

	public static string choose(Dictionary<string, int> map){
		List<string> drawBox = new List<string>();
		foreach(string key in map.Keys){
			int count = map[key];
			for(int i=0;i<count;i++){
				drawBox.Add(key);
			}
		}
		int idx = Random.Range(0,drawBox.Count);
		return drawBox[idx];
	}
}
