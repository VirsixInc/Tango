using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	public static GameManager s_instance;

	public GameObject optionMenu;
	public GameObject pauseMenu;
	public GameObject mainCanvas; // I'm just gonna bring this along with me because otherwise it loses references to me //Maybe fix?
	public GameObject sceneTransition;
	EventSystem eventSystem;

	public static bool paused = false;

	public static float musicVolume = 8;
	public static float effectsVolume = 1;
	public AudioClip mainMenuMusic;
	public AudioClip gameMusic;
	private AudioSource myAudioSource;

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
			DestroyImmediate(sceneTransition);
			return;
		}
		DontDestroyOnLoad (gameObject);
		DontDestroyOnLoad (optionMenu);
		DontDestroyOnLoad (pauseMenu);
		DontDestroyOnLoad (mainCanvas);
		DontDestroyOnLoad(sceneTransition);
		transform.FindChild ("EventSystem").gameObject.SetActive (true);

		eventSystem.SetSelectedGameObject(mainCanvas.transform.FindChild ("StartGame").gameObject);
		eventSystem.UpdateModules ();
		myAudioSource = GetComponent<AudioSource>();

		// Audio
		myAudioSource.clip = mainMenuMusic;
		myAudioSource.Play();

		sceneTransition.SetActive (false);
	}

	void OnLevelWasLoaded() {
		if (Application.loadedLevelName == "MainMenu") {
			// Audio
			myAudioSource.clip = mainMenuMusic;
			myAudioSource.Play();

//			TogglePauseMenu ();
			if(pauseMenu.activeSelf == true) {
				TogglePauseMenu();
			}
			mainCanvas.SetActive(true);
			eventSystem.SetSelectedGameObject(mainCanvas.transform.FindChild ("StartGame").gameObject);
			myAudioSource = GetComponent<AudioSource>();
			
			// Audio
			myAudioSource.clip = mainMenuMusic;
			myAudioSource.Play();
		} else {
			// Audio
			if( myAudioSource.clip != gameMusic || !myAudioSource.isPlaying) {
				myAudioSource.clip = gameMusic;
				myAudioSource.Play();
			}
		}
		sceneTransition.SetActive (false);
	}
	
	void Update () {
		if (Application.loadedLevelName != "MainMenu" && Application.loadedLevelName != "Ending") {
			if(Input.GetButtonDown("Pause Stick 1") || Input.GetButtonDown("Pause Stick 2")) {
				TogglePauseMenu();
			}
		}
		if(Application.loadedLevelName == "Ending") {
			if(Input.GetButtonDown("Pause Stick 1") || Input.GetButtonDown("Pause Stick 2")) {
				NextTransition();
			}
		}
		if(Input.GetKeyDown(KeyCode.Q)) {
			NextTransition();
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
			redTeleportOn = false;
			blueTeleportOn = false;
		}

//		print (color.ToString () + " on!");
	}

	public static void TeleporterOff(ColorComponent.pColor color) {
		if(color == ColorComponent.pColor.red) {
			redTeleportOn = false;
		} else if(color == ColorComponent.pColor.blue) {
			blueTeleportOn = false;
		}
//		print (color.ToString () + " off!");
	}

	public void TogglePauseMenu() {
		if(pauseMenu.gameObject.activeSelf) {
			pauseMenu.SetActive (false);
			paused = false;
//			EventSystem.current.SetSelectedGameObject (mainCanvas.transform.FindChild ("StartGame").gameObject);
		} else {
 			pauseMenu.SetActive(true);
			paused = true;
			eventSystem.SetSelectedGameObject (pauseMenu.transform.FindChild ("Resume").gameObject);
		}

		if(paused)
			Time.timeScale = 0f;
		else
			Time.timeScale = 1f;
	}

	public delegate void ActionDelegate();

	IEnumerator FadeInAndDoAction(ActionDelegate action) {
		GameObject background = sceneTransition.transform.FindChild ("Background").gameObject;
		sceneTransition.SetActive (true);

//		background.animation["FadeInOut"].time = 0;
//		background.animation["FadeInOut"].speed = 1;
//		background.animation.Play ("FadeInOut");

		float timer = 0f;

		while (timer < 1f) {
			timer += Time.deltaTime * 2f;
			Color color = background.GetComponent<Image>().color;
			color.a = Mathf.Lerp(0f, 1f, timer);

			if(timer > 1f) {
				timer = 1f;
				color.a = Mathf.Lerp(0f, 1f, color.a);
			}
			background.GetComponent<Image>().color = color;
			yield return null;
		}
		action ();
	}

	IEnumerator FadeOutAndDoAction(ActionDelegate action) {
		GameObject background = sceneTransition.transform.FindChild ("Background").gameObject;
//		sceneTransition.SetActive (true);
//		
//		background.animation["FadeInOut"].time = background.animation["FadeInOut"].length;
//		background.animation["FadeInOut"].speed = -1;
//		background.animation.Play ("FadeInOut");
//
		yield return new WaitForSeconds (background.animation ["FadeInOut"].length);
		sceneTransition.SetActive (false);
//		action ();
	}

//	IEnumerator FadeOut() {
//
//	}

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
		NextTransition ();

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
		s_instance.NextTransition ();
//		s_instance.FadeAndDoAction (false, s_instance.Next);
	}

	public void NextTransition() {
		StartCoroutine ("FadeInAndDoAction", (ActionDelegate)Next);
	}

	public void Next() {
		foreach(MonoBehaviour obj in FindObjectsOfType<MonoBehaviour>()) {
			obj.StopAllCoroutines();
		}

		if(Application.loadedLevel < Application.levelCount - 1) {
			Application.LoadLevel (Application.loadedLevel + 1);
		} else {
			Menu ();
		}
		if (mainCanvas.activeSelf)
			mainCanvas.SetActive (false);
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