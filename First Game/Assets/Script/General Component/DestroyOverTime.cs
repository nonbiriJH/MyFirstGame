using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float destroyTimeSecond;
    private float countDown;

    // Start is called before the first frame update
    void Start()
    {
        countDown = destroyTimeSecond;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
