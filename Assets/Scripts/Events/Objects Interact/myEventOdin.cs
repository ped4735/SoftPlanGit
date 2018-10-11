using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;


[System.Serializable]
public struct Dialog
{
   public string text;
   public float duration;

}



[System.Serializable]
public class myEventOdin
{
    public Actions action;
    public GameObject actionButton;
    public string name;
    public bool destroyAfterClick;
    //Posição para evento de movimentar gameObj
    [ShowIf("action", Actions.Mover)]
    public Transform finalPosition;
    [ShowIf("action", Actions.Mover)]
    public bool continueToMove;

    [Space(15)]
    [ShowIf("action", Actions.ShowHIde)]
    public List<GameObject> objetosToDisable = new List<GameObject>();    
    [ShowIf("action", Actions.ShowHIde)]
    public List<GameObject> objetosToEnable = new List<GameObject>();

    [Space(15)]
    [ShowIf("action", Actions.addEvents)]
    public List<InteractableOdin> objToAddEvents = new List<InteractableOdin>();
    [ShowIf("action", Actions.addEvents)]
    public List<myEventOdin> eventsToadd = new List<myEventOdin>();

    [Space(15)]
    [ShowIf("action", Actions.UnityEvents)]
    public UnityEvent unityEvents;


    
    //EXTRAS
    /*[Space(15)]
    //Dialog Box
    public bool showDialog;
    [ShowIf("showDialog")]
    public bool playEventAfterConversation;
    [ShowIf("showDialog")]
    public string nameConversation;
    [HideInInspector]
    public bool reachEndConversation;
    
    [Space(15)]
    //Show animations
    public bool Animation;
    [ShowIf("Animation")]
    public List<Animations> animationsWhenStart = new List<Animations>();*/
    /*[ShowIf("showAnimation")]
    public bool stopWhenExitInteract;
    [ShowIf("showAnimation"), ShowIf("stopWhenExitInteract")]
    public List<Animations> animationsWhenExit = new List<Animations>();*/
    
}