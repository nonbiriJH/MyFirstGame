using UnityEngine;

public class EvilBladeDestroy : DestroyOverTime
{

    // Update is called once per frame
    public override void Update()
    {
        if (this.gameObject.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
        {
            base.Update();
        }
    }
}
