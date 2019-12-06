using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runtimeController : MonoBehaviour
{

    Animator PLAnimator;

    void Start()
    {
        PLAnimator = GetComponent<Animator>();
        PLAnimator.runtimeAnimatorController = Resources.Load("Assets/Resources/System/PLController") as RuntimeAnimatorController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
