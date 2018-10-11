using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityEvent_HideShowObjects : MonoBehaviour {

    public List<GameObject> objetosToDisable = new List<GameObject>();
    public List<GameObject> objetosToEnable = new List<GameObject>();


    public void PlayUnityEvent()
    {
                
        foreach (GameObject obj in objetosToEnable)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in objetosToDisable)
        {
            obj.SetActive(false);
        }
        
    }
}
