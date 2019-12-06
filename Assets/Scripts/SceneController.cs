using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Object poseBody_prefab;
    public Animator anim;
    private bool confirmed = false;
    private bool arrived = false;
    public GameObject locationObj;
    // referring to the pop up window indicated you are within range of FFP
    public GameObject popup;

    public void ConfirmedArrival()
    {
        confirmed = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (!arrived && !confirmed)
        {
            if (locationObj.GetComponent<GeoLocation>().GoalDist() <= 10)
            {
                arrived = true;
                popup.SetActive(true);
            }
        }
        RaycastHit hit;

        // Check if mouse has been clicked
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        //if (Input.GetMouseButton(0))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            //if (true)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;
                //Vector2 touchPosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                if (Physics.Raycast(ray, out hit, 500))
                {

                    if (hit.collider.name.Contains("diamond"))
                    {
                        Debug.Log(hit.collider.name);
                        Interact(hit);
                    }

                }
            }

        }
    }

    void Interact(RaycastHit hit)
    {
        // If clicked on Start Platform, set the platform to false and update the instructions text
        if (hit.collider.name.Contains("diamond"))
        {
            Debug.Log("Hit");
            string poseNumber = hit.collider.name.Substring(7);
            hit.collider.gameObject.SetActive(false);

            poseBody_prefab = Resources.Load("Prefabs/Pose"+poseNumber+"Animation"); 

            GameObject poseBody = Instantiate(poseBody_prefab, hit.collider.transform.position , Quaternion.identity) as GameObject;


            Animator animator = poseBody.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load("Prefabs/Pose" + poseNumber + "AnimationController") as RuntimeAnimatorController;


            if (null != animator)
            {
                Debug.Log(animator);
                animator.Play("anim");
            }
            //poseBody.gameObject.SetActive(false);
            //hit.collider.gameObject.SetActive(true);
        }
    }
}
