using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    
    private string _nowPlaying;
    
    // Use this for initialization
    void Start () {
        // No music to start with
        _nowPlaying = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayTheme(string newTheme) {
        StartCoroutine(FadeAudio(newTheme));
    }

    private IEnumerator FadeAudio(string newAudio) {
        if (!string.IsNullOrEmpty(_nowPlaying)) {
            print("stopping audio: " + _nowPlaying);
        }

        // Fade out old audio while the player drinks
        yield return new WaitForSeconds(1);

        print("starting audio: " + newAudio);
        _nowPlaying = newAudio;
    }
}
