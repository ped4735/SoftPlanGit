using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Animator))]
public class OptionsPanel : MonoBehaviour {

    public static OptionsPanel instance;

    private Animator anim;
    [HideInInspector]
	public bool isShow = false;
    
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }

        anim = GetComponent<Animator>();

    }

    
    public Transform getTransform()
    {
        return transform;
    }

    public void clearOptions()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
        

    public void showOptions()
    {
        anim.Play("FadeIn");
		isShow = true;
    }

    public void hideOptions()
    {
        anim.Play("FadeOut");
		isShow = false;
    }
}
