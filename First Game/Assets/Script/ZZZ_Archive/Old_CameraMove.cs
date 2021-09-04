using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Old_CamaraMovement : MonoBehaviour
{

    [Header("Player Following")]
    public Transform target;
    public float smoothing;
    [Header("Global Camera Min Max")]
    public vectorValue globalMaxPosition;
    public vectorValue globalMinPosition;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, globalMinPosition.runtimeValue.x, globalMaxPosition.runtimeValue.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, globalMinPosition.runtimeValue.y, globalMaxPosition.runtimeValue.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }

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
