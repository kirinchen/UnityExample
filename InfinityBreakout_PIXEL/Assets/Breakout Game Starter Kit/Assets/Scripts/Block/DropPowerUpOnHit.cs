using UnityEngine;

//Make sure there is always a BoxCollider component on the GameObject where this script is added.
using System.Collections.Generic;


[RequireComponent(typeof(BoxCollider))]
public class DropPowerUpOnHit : MonoBehaviour
{
	private static string NONE = "N";
	private static string EXTRA = "E";
	private static string SMALL = "S";
	private static string BIG = "B";


	public float probability = 0.25f;
    //Every powerup needs to be derived/inherit from PowerUpBase to ensure consistent behaviour
    public PowerUpBase[] PowerUpPrefab;
	public PowerUpBase extraPowerUp;
	public PowerUpBase smallPowerUp;
	public PowerUpBase bigPowerUp;



    //OnCollision create the powerup
    void OnCollisionEnter(Collision c)
    {

		PowerUpBase pub = choosePowerUp ();
		if(pub!=null){
			GameObject.Instantiate(pub, this.transform.position, Quaternion.identity);
		}

    }

	private PowerUpBase choosePowerUp(){
		Block b = GetComponent<Block> ();
		Player p = Player.getInstance ();
		int spend = (int)(Time.time - p.startAt);

		int noneC = 50 + p.level + (int)(b.getHp () * 5) + (spend);
		int extraC = 10 + (10 - (p.level*2));
		int bigC = 10 + (10 - (p.level*2));
		int smallC = 10 + p.level ;
		Dictionary<string, int> map = new Dictionary<string,int >();
		map.Add (NONE,noneC);
		map.Add (EXTRA , extraC);
		map.Add (BIG,bigC);
		map.Add (SMALL , smallC);
		string s = GlobalMethod.choose (map);
		switch (s) {
		case "N":
			return null;
		case "E" :
			return extraPowerUp;
		case "B" :
			return bigPowerUp;
		case "S" :
			return smallPowerUp;
		}
		throw new UnityException();
	}




}