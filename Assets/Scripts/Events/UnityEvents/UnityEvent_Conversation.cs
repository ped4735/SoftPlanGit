using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public struct ConversationsEvent
{
    public int conversationID;
    public UnityEvent eventsToPlay;
}



public class UnityEvent_Conversation : MonoBehaviour {

  
    public List<string> nameConversation;
    
    public bool playEventsWhenEndConversation;
    [ShowIf("playEventsWhenEndConversation")]
    public List<ConversationsEvent> eventsToStart =  new List<ConversationsEvent>();


    GameObject player;
    private UnityEvent currentEventsToPlay;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void PlayUnityEvent(int conversationIndex)
    {
        DialogueManager.instance.GetComponent<DialogueManager>().StartConversation(nameConversation[conversationIndex]);
        Manager.instance.StopPlayerControlls(true);
        GetComponent<InteractableOdin>().OnDefocusedConversation();

        if (playEventsWhenEndConversation)
        {
            foreach (ConversationsEvent evt in eventsToStart)
            {
                if (evt.conversationID == conversationIndex)
                {
                    currentEventsToPlay = evt.eventsToPlay;
                    DialogueManager.instance.EndConversationEvent += EndConversation;
                }
            }
        }
    }


    public void EndConversation()
    {
        player.GetComponent<PlayerMotor>().enabled = true;
        currentEventsToPlay.Invoke();
        
    }

}
