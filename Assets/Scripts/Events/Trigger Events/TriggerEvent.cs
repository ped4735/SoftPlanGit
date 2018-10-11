using UnityEngine;
using UnityEngine.Events;


//Classe ira tratar de eventos quando entrar no start do personagem ou quando entrar de trigger de alguma area

public class TriggerEvent : MonoBehaviour {

    public bool eventOnStart;
    public bool eventOnTriggerEnter;
    public bool eventOnTriggerExit;

    public UnityEvent eventosToStart;
    

	// Use this for initialization
	void Start () {

        if (eventOnStart)
        {
            eventosToStart.Invoke();
        }
	}

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.name + " colidiu!");

        if (eventOnTriggerEnter)
        {
            eventosToStart.Invoke();
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        if (eventOnTriggerExit)
        {
            eventosToStart.Invoke();
        }

    }


   
}

