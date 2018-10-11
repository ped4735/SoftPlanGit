using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Animations
{
    public Animator animator;
    public AnimationClip anim;
}

public class UnityEvent_Animations : MonoBehaviour {

    public List<Animations> animations;

    // Todos os eventos devem possuir este método.
    public void PlayUnityEvent()
    {
        foreach (Animations a in animations)
        {
            a.animator.Play(a.anim.name);
        }
    }
}
