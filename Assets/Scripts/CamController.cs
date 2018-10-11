using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

    [SerializeField] private GameObject player;       //Public variable to store a reference to the player game object    
    private Transform target;
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    [SerializeField] private Space offsetPositionSpace = Space.World;
    [SerializeField] private bool lookAt = true;


    private void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (player == null)
        {
            Debug.LogWarning("Missing target ref !", this);
            return;
        }

        target = player.transform;

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offset);
        }
        else
        {
            transform.position = target.position + offset;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
            //transform.LookAt(target, Vector3.up);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}