using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public static DialogueManager instance;

	private DataConversation allConversations;
	public string[] aux;
    [HideInInspector]
    public GameObject box;
	private int sentence;

    private InteractableOdin objcInteractable;
    private myEventOdin eventPlaying;

    private bool isUnityEvent;
    [HideInInspector]
    public bool reachEndConversation;


    public delegate void EndConversation();
    public event EndConversation EndConversationEvent;

    // Use this for initialization
    void Start () {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }


		//pega a referencia para os textos do DataController
		allConversations = FindObjectOfType<DataController> ().allConversations;
		box = gameObject.transform.GetChild (0).gameObject;
		sentence = 0;

    }
	

	/*public void StartConversation(myEventOdin eventOdin, InteractableOdin interact){
        //liga a caixa de dialogo
        isUnityEvent = false;
        box.SetActive(true);
        reachEndConversation = false;
        objcInteractable = interact;
        eventPlaying = eventOdin;
        //procura o dialogo correto
        for (int i=0;i<allConversations.conversation.Length-1;i++){
			if (allConversations.conversation [i].name == eventOdin.nameConversation) {
				// quando achar a conversa correta
				aux = allConversations.conversation[i].text;
			}
		}
        OptionsPanel.instance.hideOptions();
        NextText ();
	}*/

    public void StartConversation(string nameConversation)
    {
        isUnityEvent = true;
        //liga a caixa de dialogo
        reachEndConversation = false;
        box.SetActive(true);
        //procura o dialogo correto
        for (int i = 0; i < allConversations.conversation.Length - 1; i++)
        {
            if (allConversations.conversation[i].name == nameConversation)
            {
                // quando achar a conversa correta
                aux = allConversations.conversation[i].text;
            }
        }

        //OptionsPanel.instance.hideOptions();
        NextText();

    }



    public void EndOfConversation(){
		sentence = 0;
        reachEndConversation = true; 
        /*if (!isUnityEvent)
            eventToPlay();*/

        if(EndConversationEvent != null)
            EndConversationEvent.Invoke();
        EndConversationEvent = null;

        box.SetActive(false);
        Manager.instance.StopPlayerControlls(false);
    }

    /*private void eventToPlay()
    {
        if (eventPlaying.playEventAfterConversation)
        {

            eventPlaying.reachEndConversation = true;

            switch (eventPlaying.action)
            {
                case Actions.Nothing:
                    objcInteractable.doNothing(eventPlaying);
                    break;
                case Actions.Destroy:
                    objcInteractable.destroyObj(eventPlaying);
                    break;
                case Actions.Mover:
                    objcInteractable.moverObj(eventPlaying);
                    break;
                case Actions.Pegar:
                    objcInteractable.pegarObj(eventPlaying);
                    break;
                case Actions.ShowHIde:
                    objcInteractable.hideAndSHow(eventPlaying);
                    break;
                case Actions.addEvents:
                    objcInteractable.addEvents(eventPlaying);
                    break;
                case Actions.UnityEvents:
                    objcInteractable.startUnityEvent(eventPlaying);
                    break;
                default:
                    break;
            }
        }
    }*/

    public void NextText(){
		if (sentence < aux.Length) {
			box.transform.GetChild (0).GetComponentInChildren<TextMeshProUGUI> ().text = System.Text.RegularExpressions.Regex.Unescape(aux[sentence]);
			sentence += 1;
		} else {
			EndOfConversation ();
		}

	}
}
