using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
//	enum State {
//		Intro, Main
//	}
//
//	State state;

	public static GameManager s_instance;

	[System.NonSerialized]
	public bool player1Finished, player2Finished;

	public GameObject optionMenu;
	public GameObject pauseMenu;
	public GameObject mainCanvas; // I'm just gonna bring this along with me because otherwise it loses references to me //Maybe fix?

	public static bool paused = false;

	public static float musicVolume = 8;
	public static float effectsVolume = 8;

	void Start () {
		if(s_instance == null) {
			s_instance = this;
//			GameObject mainCanvas = GameObject.Find("MainCanvas");
//			mainCanvas.transform.FindChild("StartGame").GetComponent<Button>().onClick.RemoveAllListeners();
//			mainCanvas.transform.FindChild("StartGame").GetComponent<Button>().onClick.AddListener(delegate{Next();});
		} else {
			Destroy(gameObject);
//			OptionMenu[] options = FindObjectsOfType<OptionMenu>();
//			for(int i = 0; i < options.Length; i++) {
//				if(options[i].gameObject != optionMenu)
			Destroy(optionMenu);
//			}
//			PauseMenu[] pauses = FindObjectsOfType<PauseMenu>();
//			for(int i = 0; i < pauses.Length; i++) {
//				if(pauses[i].gameObject != pauseMenu)
			Destroy(pauseMenu);
//			}
//
//			Canvas[] mainCanvases = FindObjectsOfType<Canvas>();
//			for(int i = 0; i < mainCanvases.Length; i++) {
//				if(mainCanvases[i].name == "MainCanvas") {
//					if(mainCanvases[i].gameObject.GetInstanceID() != mainCanvas.GetInstanceID()) {
			Destroy(mainCanvas);
		//					}
//				}
//			}
			return;
		}
		DontDestroyOnLoad (gameObject);
		DontDestroyOnLoad (optionMenu);
		DontDestroyOnLoad (pauseMenu);
		DontDestroyOnLoad (mainCanvas);
		transform.FindChild ("EventSystem").gameObject.SetActive (true);
	}

	void OnLevelWasLoaded() {
		if (Application.loadedLevelName == "MainMenu") {
			TogglePauseMenu ();
			mainCanvas.SetActive(true);
		}
	}
	
	void Update () {
		if (Application.loadedLevelName != "MainMenu" && Application.loadedLevelName != "Ending") {
			if(Input.GetButtonDown("Pause")) {
				paused = !paused;
				TogglePauseMenu();
				if(paused)
					Time.timeScale = 0f;
				else
					Time.timeScale = 1f;
			}
		}
	}

	public void TogglePauseMenu() {
		if(pauseMenu.gameObject.activeSelf)
			pauseMenu.SetActive (false);
		else
			pauseMenu.SetActive(true);
	}

	public void ToggleOptionMenu() {
		if(optionMenu.gameObject.activeSelf)
			optionMenu.SetActive (false);
		else
			optionMenu.SetActive(true);
	}

	public void StartGame() {
		mainCanvas.SetActive (false);
		Next ();
	}

	public static void ExitGame() {
		s_instance.Exit ();
	}

	public void Exit() {
		Application.Quit ();
	}

	public static void RestartLevel() {
		s_instance.Restart();
	}

	public void Restart() {
		foreach(MonoBehaviour obj in FindObjectsOfType<MonoBehaviour>()) {
			obj.StopAllCoroutines();
		}
		int currentLevel = Application.loadedLevel;
		Application.LoadLevel (currentLevel);
		TogglePauseMenu ();
	}

	public static void NextLevel() {
		s_instance.Next ();
	}

	public void Next() {
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
	public void Menu() {
		foreach(MonoBehaviour obj in FindObjectsOfType<MonoBehaviour>()) {
			obj.StopAllCoroutines();
		}
		Application.LoadLevel ("MainMenu");
	}
}
