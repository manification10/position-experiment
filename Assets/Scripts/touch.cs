using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour
{
    public Object poseBody_prefab;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;

        // Check if mouse has been clicked
        //if (Input.touchCount > 0 || Input.GetMouseButton(0))
        if (Input.GetMouseButton(0))
        {
            //if (Input.GetTouch(0).phase == TouchPhase.Began)
            if(true)
            {
                //Vector2 touchPosition = Input.GetTouch(0).position;
                Vector2 touchPosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                if (Physics.Raycast(ray, out hit, 500))
                {
                    Debug.Log("Hit collider");
                    Debug.Log(hit.collider.name);
                    if (hit.collider.name == "diamond1")
                    {
                        Debug.Log("Clicked by name");
                        Interact(hit);
                    }

                    //OR with Tag

                    //if (hit.collider.CompareTag("diamond"))
                    //{
                    //    Debug.Log("Clicked by Tag");
                    //    Interact(hit);
                    //}
                }
            }

        }
    }

    void Interact(RaycastHit hit)
    {
        // If clicked on Start Platform, set the platform to false and update the instructions text
        if (hit.collider.name == "diamond1")
        {
            Debug.Log("Hitdiamond");
            hit.collider.gameObject.SetActive(false);

            poseBody_prefab = Resources.Load("Prefabs/Pose1Animation"); // Assets/Resources/Prefabs/prefab1.FBX
            GameObject poseBody = Instantiate(poseBody_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;


            Animator animator = poseBody.gameObject.GetComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load("Prefabs/Pose1AnimationController") as RuntimeAnimatorController;

            if (null != animator)
            {
                Debug.Log("Playing anim");
                animator.Play("anim");
            }
            //poseBody.gameObject.SetActive(false);
            //hit.collider.gameObject.SetActive(true);
        }
    }
}

