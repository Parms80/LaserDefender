using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	public AudioClip menuClip;
	public AudioClip gameplayClip;

	private AudioSource music;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			SetupAndStartMusic ();
		}
	}

	void SetupAndStartMusic() {
		music = GetComponent<AudioSource> ();
		music.clip = menuClip;
		music.loop = true;
		music.Play ();		
	}

	void OnLevelWasLoaded(int level) {
		Debug.Log ("MusicPlayer: loadededlevel "+level);
		if (!music || SceneManager.GetActiveScene ().name == "Start") {
			return;
		}
			
		Debug.Log ("music.clip.name = " + music.clip.name + ", menuClip.name = " + menuClip.name);
		music.Stop ();
		if (level == 0) {
			music.clip = menuClip;
		}
		if (level == 1) {
			music.clip = gameplayClip;
		}
		if (level == 2) {
			music.clip = menuClip;
		}
		music.loop = true;
		music.Play ();
	}
}
