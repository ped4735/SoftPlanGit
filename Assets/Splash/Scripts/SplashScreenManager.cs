using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour {

    public RawImage[] logos;
    public float delay = 2f;
    public float fadeTime = 0.2f;
    public float zoom = 1f;

    int index = 0;
    RawImage curr;
    float startTime;

	// Use this for initialization
	void Start () {
		foreach (RawImage i in logos) {
            i.gameObject.SetActive(true);
            SetAlpha(i, 0);
        }
        curr = logos[index];
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float a;
        float diff = Time.time - startTime;
        float aDiff = delay - diff;
        if (diff <= fadeTime) {
            a = diff / fadeTime;
        } else if(aDiff <= fadeTime) {
            a = aDiff / fadeTime;
        } else {
            a = 1;
        }
        curr.gameObject.transform.localScale += new Vector3(zoom, zoom, zoom);
        SetAlpha(curr, a);
        if (diff > delay) {
            startTime = Time.time;
            SetAlpha(curr, 0);
            index = ++index % logos.Length;
            curr = logos[index];
            if(index == 0) {
                SceneManager.LoadScene("TitleScreen");
            }
        }
	}

    void SetAlpha(RawImage i, float alpha) {
        Color c = i.color;
        c.a = alpha;
        i.color = c;
    }
}
