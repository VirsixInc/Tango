using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
	public GameObject sceneTransition;
	EventSystem eventSystem;

	public static bool paused = false;

	public static float musicVolume = 8;
	public static float effectsVolume = 8;

	static bool redTeleportOn = false, blueTeleportOn = false;

	void Awake () {
		if(s_instance == null) {
			s_instance = this;
			eventSystem = transform.FindChild("EventSystem").GetComponent<EventSystem>();
		} else {
			DestroyImmediate(optionMenu);
			DestroyImmediate(pauseMenu);
			DestroyImmediate(mainCanvas);
			DestroyImmediate(gameObject);
			return;
		}
		DontDestroyOnLoad (gameObject);
		DontDestroyOnLoad (optionMenu);
		DontDestroyOnLoad (pauseMenu);
		DontDestroyOnLoad (mainCanvas);
		transform.FindChild ("EventSystem").gameObject.SetActive (true);

		eventSystem.SetSelectedGameObject (mainCanvas.transform.FindChild ("StartGame").gameObject);
	}

	void OnLevelWasLoaded() {
		if (Application.loadedLevelName == "MainMenu") {
			TogglePauseMenu ();
			mainCanvas.SetActive(true);
			eventSystem.SetSelectedGameObject(mainCanvas.transform.FindChild ("StartGame").gameObject);
		}
	}
	
	void Update () {
		if (Application.loadedLevelName != "MainMenu" && Application.loadedLevelName != "Ending") {
			if(Input.GetButtonDown("Pause Stick 1") || Input.GetButtonDown("Pause Stick 2")) {
				paused = !paused;
				TogglePauseMenu();
				if(paused)
					Time.timeScale = 0f;
				else
					Time.timeScale = 1f;
			}
		}
	}

	public static void TeleporterOn(ColorComponent.pColor color) {
		if(color == ColorComponent.pColor.red) {
			redTeleportOn = true;
		} else if(color == ColorComponent.pColor.blue) {
			blueTeleportOn = true;
		}

		if(redTeleportOn && blueTeleportOn) {
			NextLevel();
		}

		print (color.ToString () + " on!");
	}

	public static void TeleporterOff(ColorComponent.pColor color) {
		if(color == ColorComponent.pColor.red) {
			redTeleportOn = false;
		} else if(color == ColorComponent.pColor.blue) {
			blueTeleportOn = false;
		}
		print (color.ToString () + " off!");
	}

//	void Teleporter() {
//
//	}

	public void TogglePauseMenu() {
		if(pauseMenu.gameObject.activeSelf) {
			pauseMenu.SetActive (false);
//			EventSystem.current.SetSelectedGameObject (mainCanvas.transform.FindChild ("StartGame").gameObject);
		} else {
 			pauseMenu.SetActive(true);
			eventSystem.SetSelectedGameObject (pauseMenu.transform.FindChild ("Resume").gameObject);
		}
	}

	public delegate void ActionDelegate();

	IEnumerator FadeAndDoAction(bool fadeIn, ActionDelegate action) {
		GameObject background = sceneTransition.transform.FindChild ("Background").gameObject;
		if(fadeIn) {
			background.animation["FadeInOut"].time = background.animation["FadeInOut"].length;
			background.animation["FadeInOut"].speed = -1;
			background.animation.Play ();
		} else {
			background.animation["FadeInOut"].time = 0;
			background.animation["FadeInOut"].speed = 1;
			background.animation.Play ();
		}
		yield return new WaitForSeconds (background.animation ["FadeInOut"].length);
		action ();
	}

	public void ToggleOptionMenu() {
		if(optionMenu.gameObject.activeSelf) {
			optionMenu.SetActive (false);
			if(Application.loadedLevelName == "MainMenu") {
				mainCanvas.SetActive(true);
				eventSystem.SetSelectedGameObject (mainCanvas.transform.FindChild ("Options").gameObject);
			} else {
				pauseMenu.SetActive(true);
				eventSystem.SetSelectedGameObject (pauseMenu.transform.FindChild ("Options").gameObject);
			}
		} else {
			optionMenu.SetActive(true);
			mainCanvas.SetActive(false);
			pauseMenu.SetActive(false);
			eventSystem.SetSelectedGameObject (optionMenu.transform.FindChild ("Return").gameObject);
		}
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
//		s_instance.FadeAndDoAction (false, s_instance.Next);
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
