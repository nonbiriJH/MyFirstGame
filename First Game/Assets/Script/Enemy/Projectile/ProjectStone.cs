using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectStone : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    public Rigidbody2D myRigidBody;
    private float destoryTimeSecond;


    // Start is called before the first frame update

    public virtual void Start()
    {
        destoryTimeSecond = destroyTime;
    }

    // Update is called once per frame
    void Update()
    {
        //count down destroy
        destoryTimeSecond -= Time.deltaTime;
        if (destoryTimeSecond < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector3 direction)
    {
        Debug.Log("start");
        myRigidBody.velocity = direction * speed;//Once called make displacement every frame
    }

    //When collide with other rigid body, destroy
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger || other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    
}
