using System.Collections.Generic;
using UnityEngine;

public class UnityEvent_AddEventsToTarget : MonoBehaviour
{

    public List<InteractableOdin> objToAddEvents = new List<InteractableOdin>();
    public List<myEventOdin> eventsToadd = new List<myEventOdin>();

    
    public void PlayUnityEvent()
    {

        foreach (InteractableOdin objAddEvent in objToAddEvents)
        {
            foreach (myEventOdin eventOdin in eventsToadd)
            {
                objAddEvent.events.Add(eventOdin);
            }
        }

    }

}




    




