using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Level : MonoBehaviour {

	[SerializeField] float delayInSeconds = 2f;

	public void LoadStartMenu() {
		SceneManager.LoadScene(0);
	}

	public void LoadGame() {
		SceneManager.LoadScene("Game");
		FindObjectOfType<GameSession>().ResetGame();
	}

	public void LoadGameOver() {
		StartCoroutine(WaitAndLoad());
	}

	IEnumerator WaitAndLoad() {
		yield return new WaitForSeconds(delayInSeconds);
		SceneManager.LoadScene("Game Over");
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void PostHighScore() {
		Upload();
	}

	public void Upload() {
		string scoreToSend = FindObjectOfType<GameSession>().GetScore().ToString();
		string nameToSend = FindObjectOfType<InputField>().text;

        UnityWebRequest www = UnityWebRequest.Post("https://shooterhighscores.herokuapp.com/" + scoreToSend + "/" + nameToSend, "");
		www.SendWebRequest();

		LoadStartMenu();
	}

}
