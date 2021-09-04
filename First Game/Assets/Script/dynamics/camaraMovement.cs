using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraMovement : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void StartAnimScreenKick()
    {
        animator.SetBool("screenKick", true);
        StartCoroutine(ScreenKickCo());
    }

    private IEnumerator ScreenKickCo()
    {
        yield return null;
        animator.SetBool("screenKick", false);
    }
}
