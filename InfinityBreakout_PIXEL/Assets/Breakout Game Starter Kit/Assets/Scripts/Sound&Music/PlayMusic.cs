using UnityEngine;

//Make sure there is always an AudioSource component on the GameObject where this script is added.
[RequireComponent(typeof(AudioSource))]
public class PlayMusic : MonoBehaviour
{
    //Make the AudioClip configurable in the editor
    public AudioClip BackgroundMusic;

    //Start is called one time when the scene has been loaded
    void Start()
    {
		AudioSource audio = GetComponent<AudioSource> ();
        audio.loop = true;
        audio.clip = BackgroundMusic;
        audio.Play();
    }
}