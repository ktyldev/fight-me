using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
	public Text startText, titleText, loadingText;
	float timer;
	bool ready;

	// Use this for initialization
	void Start () {
		StartCoroutine(StartDelay());
	}
	
	// Update is called once per frame
	void Update () {
		if (ready) {
		    timer += Time.deltaTime;
		    if (timer >= 0.5f) {
		        startText.enabled = !startText.enabled;
                timer = 0.0f;
		    }

			if (Input.GetKeyDown(KeyCode.Return)) {
			    startText.enabled = false;
				titleText.enabled = false;
				loadingText.enabled = true;
				ready = false;
                StartCoroutine(LoadLevel());
			}
		}
	}

	IEnumerator StartDelay() {
		yield return new WaitForSeconds(2.0f);
		ready = true;
	}

	IEnumerator LoadLevel()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync("main");
		while (!operation.isDone)
			yield return null;
	}
}
