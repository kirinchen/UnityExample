
using System;
using UnityEngine;

public class Player
{
	private static Player instance = new Player();

	public float startAt {
		 get;
		private set;
	}

	public int hp{
		get; set;
	}

	public int level{
		get; set;
	}


	private Player ()	{
		startAt = Time.time;
	}

	public static Player getInstance(){
		return instance;
	}
}


