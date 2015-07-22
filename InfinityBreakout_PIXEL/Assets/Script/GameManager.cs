using System.Collections;
using UnityEngine;

//List of all the posible gamestates
public enum GameState
{
    NotStarted,
    Playing,
    Completed,
    Failed
}

//Make sure there is always an AudioSource component on the GameObject where this script is added.
[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    //Text element to display certain messages on
    public GUIText FeedbackText;

    //Text to be displayed when entering one of the gamestates
    public string GameNotStartedText;
    public string GameCompletedText;
    public string GameFailedText;

    //Sounds to be played when entering one of the gamestates
    public AudioClip StartSound;
    public AudioClip FailedSound;

    private GameState currentState = GameState.NotStarted;
    //All the blocks found in this level, to keep track of how many are left
    private Block[] allBlocks;
    private Ball[] allBalls;

	public GameObject servingBall;

    // Use this for initialization
    void Start()
    {
		//setupBlocks ();
        //Find all the blocks in this scene
        allBlocks = FindObjectsOfType(typeof(Block)) as Block[];

        //Find all the balls in this scene
        allBalls = FindObjectsOfType(typeof(Ball)) as Ball[];

        //Prepare the start of the level
        SwitchTo(GameState.NotStarted);

		setupWalls ();

		initPlayer ();
    }

	private void initPlayer(){
		Player.getInstance ().hp = 100;
	}

	private void setupBlocks(){
		BlockCreator bc = GetComponent<BlockCreator> ();
		bc.init ();
		bc.create ();
	}

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.NotStarted:
                    //Check if the player taps/clicks.
                    if (Input.GetMouseButtonDown(0))    //Note: on mobile this will translate to the first touch/finger so perfectly multiplatform!
                    {
						setupBlocks ();
                        for (int i = 0; i < allBalls.Length; i++)
                            allBalls[i].Launch();

                        SwitchTo(GameState.Playing);
                    }
                break;
            case GameState.Playing:
                {
                    bool allBlocksDestroyed = true;

                    //Check if all blocks have been destroyed
                    for (int i = 0; i < allBlocks.Length; i++)
                    {
                        if (!allBlocks[i].BlockIsDestroyed)
                        {
                            allBlocksDestroyed = false;
                            break;
                        }
                    }

                    //Are there no balls left?
                    if (FindObjectOfType(typeof(Ball)) == null)
						serving();
                        //

            //        if (allBlocksDestroyed)
              //          SwitchTo(GameState.Completed);
                }
                break;
            //Both cases do the same: restart the game
            case GameState.Failed:
            case GameState.Completed:
                //Check if the player taps/clicks.
                if (Input.GetMouseButtonDown(0))    //Note: on mobile this will translate to the first touch/finger so perfectly multiplatform!
                    Restart();
                break;
        }
    }

	private void serving(){
		GameObject newBall = Instantiate(servingBall) as GameObject;
		newBall.transform.position = new Vector3 (0,0,-13);
		newBall.GetComponent<Ball> ().Launch ();
	}

    //Do the appropriate actions when changing the gamestate
    public void SwitchTo(GameState newState)
    {
        currentState = newState;
		AudioSource audio = GetComponent<AudioSource>();
        switch (currentState)
        {
            default:
            case GameState.NotStarted:
                DisplayText(GameNotStartedText);
                break;
            case GameState.Playing:
                audio.PlayOneShot(StartSound);
                DisplayText("");
                break;
            case GameState.Completed:
                audio.PlayOneShot(StartSound);
                DisplayText(GameCompletedText);
                StartCoroutine(RestartAfter(StartSound.length));
                break;
            case GameState.Failed:
                audio.PlayOneShot(FailedSound);
                DisplayText(GameFailedText);
                StartCoroutine(RestartAfter(FailedSound.length));
                break;
        }
    }

    //Helper to display some text
    private void DisplayText(string text)
    {
        FeedbackText.text = text;
    }

    //Coroutine which waits and then restarts the level
    //Note: You need to call this method with StartRoutine(RestartAfter(seconds)) else it won't restart
    private IEnumerator RestartAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Restart();
    }

    //Helper to restart the level
    private void Restart()
    {
        Application.LoadLevel(0);
    }

	private void setupWalls(){
		GameObject walls = GameObject.Find ("Walls");
		float Distance = Camera.main.orthographicSize * Camera.main.aspect ;
		// Left Wall
		GameObject left = walls.transform.FindChild ("Left").gameObject;
		Vector3 pos = left.transform.position;
		float shiftW = getBoxSize (left).x ;
		left.transform.position = new Vector3 (-(Distance+shiftW),pos.y,pos.z);
		//Right Wall
		GameObject right = walls.transform.FindChild ("Right").gameObject;
		pos = right.transform.position;
		shiftW = getBoxSize (right).x ;
		right.transform.position = new Vector3 ((Distance+shiftW),pos.y,pos.z);
		//Top Wall
		GameObject top = walls.transform.FindChild ("Top").gameObject;
		pos = top.transform.position;
		float shiftH = getBoxSize (top).y ;
		top.transform.position = new Vector3 (pos.x,pos.y,(Camera.main.orthographicSize+shiftH));
	}



	private Vector3 getBoxSize(GameObject go){
		BoxCollider box = go.GetComponent<BoxCollider> ();
		return box.size;
	}

}