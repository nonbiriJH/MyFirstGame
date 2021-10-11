using UnityEngine;

public class DealerProjectile : MonoBehaviour
{
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
    {   //measure time between frame and trigger prject bool
        projectDelaySecond -= Time.deltaTime;
        if (projectDelaySecond < 0)
        {
            canProject = true;
            projectDelaySecond = projectDelay;
        }

        if (canProject)
        {
            Vector3 direction = target.position - transform.position;
            //Instantiate Weapon
            float spriteDegreeToLeft = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject newWeapon = Instantiate(project, transform.position, Quaternion.Euler(0, 0, spriteDegreeToLeft));
            //Move Weapon
            newWeapon.GetComponent<Movement>().MoveObject(direction);
            canProject = false;
        }

    }
}
