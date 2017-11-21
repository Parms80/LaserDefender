using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		SceneManager.LoadScene (name);

//		if (SceneManager.GetActiveScene ().name != "Win Screen") {
//			changeMusic (name);
//		}
	}

//	private void changeMusic(string name) {
//
//		GameObject musicPlayer = GameObject.Find ("Music Player");
//		AudioSource audio = musicPlayer.GetComponent<AudioSource> ();
//		MusicPlayer musicPlayerScript = musicPlayer.GetComponent<MusicPlayer> ();
//
//		if (name == "Game") {
//			audio.clip = musicPlayerScript.gameplayClip;
//		} else {
//			audio.clip = musicPlayerScript.menuClip;
//		}	
//		audio.Play ();
//	}

	public void QuitRequest() {
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
