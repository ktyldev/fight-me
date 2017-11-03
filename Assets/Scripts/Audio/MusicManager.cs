using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public float fadeSpeed;

    private GameObject _nowPlaying;
    
    void Start() {
        // No music to start with
        _nowPlaying = null;
    }
    
    public void PlayTheme(GameObject music) {
        if (_nowPlaying == null) {
            _nowPlaying = Instantiate(music, transform);
            return;
        }
        
        if (SongsAreSame(_nowPlaying, music)) 
            return;
        
        StartCoroutine(FadeAudio(music));
    }

    private bool SongsAreSame(GameObject song1, GameObject song2 /*Woo hoo!*/) {
        return song1.GetComponent<Song>().id == song2.GetComponent<Song>().id;
    }

    private IEnumerator FadeAudio(GameObject newSong) {
        var oldAudio = _nowPlaying.GetComponent<AudioSource>();
        var newAudio = Instantiate(newSong, transform).GetComponent<AudioSource>();
        newAudio.volume = 0;
        newAudio.Play();

        while (newAudio.volume < 1) {
            oldAudio.volume -= fadeSpeed;
            newAudio.volume += fadeSpeed;
            yield return new WaitForSeconds(0.001f);
        }

        oldAudio.Stop();
        var oldAudioObject = _nowPlaying;
        Destroy(oldAudioObject);
        _nowPlaying = newAudio.gameObject;
    }
}
