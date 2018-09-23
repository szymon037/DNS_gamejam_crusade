using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void StartNewGame() {
		SceneManager.LoadScene("SampleScene");
	}

	public void QuitTheGame() {
		#if UNITYEDITOR
		Debug.Break();
		#else
		Application.Quit();
		#endif
	}
}
