using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataController : MonoBehaviour {

	public DataConversation allConversations;
	private string textDataFileName = "data.json";

	// Use this for initialization
	void Start () {
		//mantem os dados persistentes
		//DontDestroyOnLoad (gameObject);
		//le o JSON
		LoadTextData();
		//carrega a primeira cena
       // SceneManager.LoadScene ("Gameplay");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void LoadTextData(){
		string filePath = Path.Combine(Application.streamingAssetsPath, textDataFileName);

		if(File.Exists(filePath))
        {
            // Le o JSON e joga todo o texto em uma string
            string dataAsJson = File.ReadAllText(filePath); 

			//Chama o JsonUyility pra ele transformar a string no Objeto dataText
            DataConversation loadedText = JsonUtility.FromJson<DataConversation>(dataAsJson);

            //Joga tudo agora na nossa variavel pública
            allConversations = loadedText;
        }else{
			Debug.Log ("Arquivo Not founD.");
		}


	}
}
