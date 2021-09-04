using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float range;
    public Transform target;

    public float projectDelay;
    public GameObject project;

    private float projectDelaySecond;
    private bool canProject;

    // Start is called before the first frame update
    void Start()
    {
        projectDelaySecond = projectDelay;
    }

    // Update is called once per frame
    void Update()
    {
        //measure time between frame and trigger prject bool
        projectDelaySecond -= Time.deltaTime;
        if (projectDelaySecond < 0)
        {
            canProject = true;
            projectDelaySecond = projectDelay;
        }

        if (canProject)
        {
            if(Vector3.Distance(target.position,transform.position) < range)
            {
                Vector3 direction = target.position - transform.position;
                direction.Normalize();
                GameObject projectStuff = Instantiate(project, transform.position, Quaternion.identity);
                projectStuff.GetComponent<ProjectStone>().Launch(direction);
                canProject = false;
            }
        }
    }

}
