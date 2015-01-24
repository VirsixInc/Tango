using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
//	enum State {
//		Intro, Main
//	}
//
//	State state;

	public static GameManager s_instance;

	[System.NonSerialized]
	public bool player1Finished, player2Finished;

	void Start () {
		if(s_instance == null) {
			s_instance = this;
		} else {
			Destroy(gameObject);
			return;
		}
	}
	
	void Update () {
		
	}

	public static void RestartLevel() {
		s_instance.Restart();
	}

	void Restart() {
		foreach(MonoBehaviour obj in FindObjectsOfType<MonoBehaviour>()) {
			obj.StopAllCoroutines();
		}
		int currentLevel = Application.loadedLevel;
		Application.LoadLevel (currentLevel);
	}

	public static void NextLevel() {
		s_instance.Next ();
	}

	void Next() {
		foreach(MonoBehaviour obj in FindObjectsOfType<MonoBehaviour>()) {
			obj.StopAllCoroutines();
		}

		if(Application.loadedLevel < Application.levelCount) {
			Application.LoadLevel (Application.loadedLevel + 1);
		} else {
			Menu ();
		}
	}

	public static void MainMenu() {
		s_instance.Menu ();
	}

	/// <summary>
	/// Stops all coroutines and goes to main menu level
	/// </summary>
	void Menu() {
		foreach(MonoBehaviour obj in FindObjectsOfType<MonoBehaviour>()) {
			obj.StopAllCoroutines();
		}
		Application.LoadLevel ("MainMenu");
	}
}
