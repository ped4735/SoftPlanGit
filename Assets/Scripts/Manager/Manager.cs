using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public static Manager instance;

    public GameObject player;

    private NavMeshSurface surface;
    private Transform currentTrans;


    private void Start()
    {
        #region SingleTon
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        } 
        #endregion

        //surface = GameObject.Find("Navmesh").GetComponent<NavMeshSurface>();

    }

    public void refreshNavMesh()
    {
        StartCoroutine(buildNavMesh());
    }

    public void startDialog(Transform npcTrans, List<Dialog> dialogs)
    {
        currentTrans = npcTrans;
        StartCoroutine(dialogCoroutine(dialogs));
    }

    IEnumerator buildNavMesh()
    {
        yield return new WaitForSeconds(0.1f);
        //surface.BuildNavMesh();
    }
    IEnumerator dialogCoroutine(List<Dialog> dialogs)
    {

        Transform anim = currentTrans.GetChild(0).GetChild(0);


        for (int i = 0; i < dialogs.Count; i++)
        {
           
            anim.GetComponentInChildren<Text>().text = dialogs[i].text;
            anim.GetComponent<Animator>().Play("FadeIn");

            Debug.Log(dialogs[i].duration);

            yield return new WaitForSeconds(dialogs[i].duration);
         

        }

        anim.GetComponent<Animator>().Play("FadeOut");

    }

    public void StopPlayerControlls(bool stop)
    {

        player.GetComponent<PlayerController>().enabled = !stop;
        Debug.Log(!stop);
    }

}
