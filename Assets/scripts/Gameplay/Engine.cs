using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") !=0) 
            animator.SetBool("IsOn", true);
        else animator.SetBool("IsOn", false);
    }
}
