using UnityEngine.EventSystems;
using UnityEngine;



[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    Camera cam;
    public int movementMask;
    public InteractableOdin focus;
    PlayerMotor motor;
        

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {


        if(EventSystem.current != null && OptionsPanel.instance.isShow || DialogueManager.instance.box.activeSelf)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
        }


        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;




            if(Physics.Raycast(ray, out hit, 100))
            {

                //Debug.Log("We click on " + hit.collider.name + hit.point);
                //if()

                //Debug.Log(hit.transform.gameObject.layer);
                //Debug.Log((int)movementMask);

                if (hit.transform.gameObject.layer == movementMask)
                {
                   

                    motor.MoveToPoint(hit.point);
                    RemoveFocus();
                }
                else
                {
                    InteractableOdin interactable = hit.collider.GetComponent<InteractableOdin>();

                    if (interactable != null)
                    {

                        SetFocus(interactable);

                    }
                }

            }
        }
    }


    void SetFocus(InteractableOdin newFocus)
    {

        if(newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        //motor.MoveToPoint(newFocus.transform.position);
        newFocus.OnFocused(transform);
        
    }


    public void RemoveFocus()
    {
        if(focus != null)
            focus.OnDefocused();
        focus = null;
        motor.StopFollowTarget();
    }

    void OnTriggerEnter(Collider coll)
    {
        print("aaaaaaaaaaa");
        if(coll.gameObject.tag == "Turnstile")
        {
            print("catraca");
            Animator anim = coll.gameObject.GetComponent<Animator>();
            anim.SetTrigger("PlayerPass");
        }
    }
}
