using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct UI_Elements
{
    public GameObject uiElement;
   
}

public class UnityEvent_CanvasEvent : MonoBehaviour {

    public List<UI_Elements> ui_Elements;

    // Todos os eventos devem possuir este método.
    public void PlayUnityEvent()
    {
        foreach (UI_Elements ui in ui_Elements)
        {
           
        }
    }


}
