using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTPCharacter : MonoBehaviour
{

    public Animator bodyAnimator;

    void Start()
    {

    }

    public void AssignAnimator()
    {
        bodyAnimator = GetComponent<Animator>();

    }

    public Animator GetBodyAnimator()
    {
        return bodyAnimator;
    }

    public void setBodyAnimator(Animator anim)
    {
        bodyAnimator = anim;
    }


}
