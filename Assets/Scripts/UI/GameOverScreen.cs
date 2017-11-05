using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {

    protected static GameOverScreen _instance;

    public GameObject victory;
    public float secondsToReload;

    void Awake() {
        _instance = this;    
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void GameOver(bool won) {
        if (won) {
            _instance.Victory();
        }

        _instance.Reload();
    }

    public void Victory() {
        Instantiate(victory, transform);
    }

    protected void Reload() {
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine() {
        yield return new WaitForSeconds(secondsToReload);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
