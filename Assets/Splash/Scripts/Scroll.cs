using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scroll : MonoBehaviour {

    // O objeto a ser oculto ao fim do scroll
    public GameObject finalDisable;

    public CanvasScaler scaler;

    // O valor de Y quando o scroll estiver finalizado
    public float finalY;

    public float speed;

    Vector3 v;

    void Start() {
        v = transform.position;
    }

    public void Begin() {
        finalDisable.SetActive(true);
        Start();
        v.y = 0;
        transform.position = v;
    }
	
	// Update is called once per frame
	void Update () {
        float ratio = Screen.width / 1920f;
        v.y += speed;
        transform.position = v;
        print(v.y);
        
        print(ratio);

        if (v.y >= finalY * ratio) {
            finalDisable.SetActive(false);
        }
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Exit()
    {
        finalDisable.SetActive(false);
    }
}
