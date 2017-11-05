using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    bool fading;
	Image bg;
	Text tutText;

	void Start() {
        bg = GetComponent<Image>();
		tutText = gameObject.GetComponentInChildren<Text>();
		StartCoroutine(TutorialFadeDelay());
	}

	// Update is called once per frame
	void Update () {
		if (fading) {
            bg.color = Color.Lerp(bg.color, new Vector4(bg.color.r, bg.color.g, bg.color.b, 0), 0.1f);
            tutText.color = Color.Lerp(tutText.color, new Vector4(tutText.color.r, tutText.color.g, tutText.color.b, 0), 0.1f);
		}
	}

	IEnumerator TutorialFadeDelay() {
		yield return new WaitForSeconds(5.0f);
		fading = true;
	}
}
