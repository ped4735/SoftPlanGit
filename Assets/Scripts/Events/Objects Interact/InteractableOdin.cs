using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.AI;

/* ------------------------------------------------------------
    Esse mono script deve ser inserido nos objetos que irão possuir interação ao ser clicados, mostrando os botões dos eventos 
---------------------------------------------------------------*/

[RequireComponent (typeof(Collider))]
public class InteractableOdin : MonoBehaviour {

    //Raio da esfera de interação com o objeto
    public float radius;
    
    //Transform onde será verificado a distancia para interação (caso null será usado do próprio gameObject) 
    //exemplo de uso: atendente fica atras do caixa, nesse caso deve ser inserido um target na frente do balcão.
    public Transform interactionTransform;

    NavMeshAgent agent;

    [Space(30)]
    public List<myEventOdin> events = new List<myEventOdin>();
    Transform player;
    Transform targetInFocus;

    bool isFocus = false;
    bool hasInteract = false;
    bool hasOptions = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update () {
        
        if (isFocus && !hasInteract)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance <= radius)
            {
                Debug.Log("INTERACT!");
                Interact();
                hasInteract = true;
            }
        }
    }

    public void Interact()
    {
        Debug.Log("Interact with " + gameObject.name);

        if (agent != null)
        {
            agent.isStopped = true;
            
            //player.GetComponent<NavMeshAgent>().isStopped = false;
        }

        player.GetComponent<NavMeshAgent>().isStopped = true;
        transform.LookAt(player.transform);
        player.transform.LookAt(transform);

        //player.LookAt(interactionTransform.parent.transform);
        OptionsPanel.instance.showOptions();
    }


    #region Eventos
    public void destroyObj(myEventOdin act)
    {
        Debug.Log("Destruiu!");

        //TestExtras(act);
        OnDefocused();
        Destroy(gameObject);
    }

    public void moverObj(myEventOdin act)
    {
        Debug.Log("Moveu!");
        transform.position = act.finalPosition.position;
        
        player.GetComponent<PlayerController>().RemoveFocus();

        if (!act.continueToMove)
            events.Remove(act);

    }

    public void pegarObj(myEventOdin act)
    {
       // TestExtras(act);
    }

    public void doNothing(myEventOdin act)
    {
       // TestExtras(act);
    }
   
    public void hideAndSHow(myEventOdin act)
    {

        //if (!act.playEventAfterConversation || act.reachEndConversation)
        //{
            foreach (GameObject obj in act.objetosToEnable)
            {
                obj.SetActive(true);
            }

            foreach (GameObject obj in act.objetosToDisable)
            {
                obj.SetActive(false);
            }
       // }

       
         //TestExtras(act);        
               

    }
    
    public void addEvents(myEventOdin act)
    {
        foreach (InteractableOdin objAddEvent in act.objToAddEvents)
        {
            foreach (myEventOdin eventOdin in act.eventsToadd)
            {
                objAddEvent.events.Add(eventOdin);
            }
        }

        //TestExtras(act);

    }
    
    public void startUnityEvent(myEventOdin act)
    {
        // if (!act.playEventAfterConversation || act.reachEndConversation)
        // {
        if (act.destroyAfterClick)
        {
            events.Remove(act);
        }


        act.unityEvents.Invoke();
       // }

        //TestExtras(act);
    }
    #endregion


    #region Focus
    public void OnDefocused()
    {

        Debug.Log("Defocused");

        if(player != null)
        {
            player.GetComponent<NavMeshAgent>().isStopped = false;
        }

        isFocus = false;
        player = null;
        hasInteract = false;
        hasOptions = false;

        OptionsPanel.instance.clearOptions();
        OptionsPanel.instance.hideOptions();

        
        if (agent != null)
            agent.isStopped = false;
    }

    public void OnDefocusedConversation()
    {

        Debug.Log("Defocused Conversation");

        isFocus = false;
        hasInteract = false;
        hasOptions = false;

        OptionsPanel.instance.clearOptions();
        OptionsPanel.instance.hideOptions();

    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteract = false;

        Debug.Log("Focused");

        if (!hasOptions)
        {

            foreach (myEventOdin evento in events)
            {
                myEventOdin act = evento;

                //GameObject temp = Instantiate(act.actionButton, OptionsPanel.instance.getTransform()) as GameObject;
                GameObject temp = Instantiate(act.actionButton, OptionsPanel.instance.getTransform()) as GameObject;
                temp.name = "Buttom " + act.name;
                temp.GetComponentInChildren<TextMeshProUGUI>().text = act.name;
                //temp.GetComponent<Button>().onClick.AddListener(() => destroyObj());
                SetSelectedListner(temp.GetComponent<Button>(), act);

            }

            hasOptions = true;
        }
    }
    #endregion

    private void SetSelectedListner(Button btn, myEventOdin act)
    {

        Actions actSelected = act.action;

        switch (actSelected)
        {
            case Actions.Nothing:
                btn.onClick.AddListener(() => doNothing(act));
                break;
            case Actions.Destroy:
                btn.onClick.AddListener(() => destroyObj(act));
                break;
            case Actions.Mover:
                btn.onClick.AddListener(() => moverObj(act));
                break;
            case Actions.Pegar:
                btn.onClick.AddListener(() => pegarObj(act));
                break;
            case Actions.ShowHIde:
                btn.onClick.AddListener(() => hideAndSHow(act));
                break;
            case Actions.addEvents:
                btn.onClick.AddListener(() => addEvents(act));
                break;
            case Actions.UnityEvents:
                btn.onClick.AddListener(() => startUnityEvent(act));
                break;
            default:
                break;
        }
    }

    //Projeta a area que o player ira interagir. [float : radius] quando selecionado na aba Scene.
    private void OnDrawGizmos()
    {

        if (interactionTransform == null)
            interactionTransform = transform;


        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    
     
}


public enum Actions
{
    Nothing,
    Destroy,
    Mover,
    Pegar,
    ShowHIde,
    addEvents,
    UnityEvents
}
