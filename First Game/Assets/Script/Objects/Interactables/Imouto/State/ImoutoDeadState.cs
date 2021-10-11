using UnityEngine;

public class ImoutoDeadState : ImoutoState
{
    public ImoutoDeadState(Imouto imouto) : base(imouto)
    {
    }

    public override void BeginState()
    {
        imouto.StopWalk();
        imouto.SetAnimParaBool("Die", true);
        BoxCollider2D[] boxCollider2Ds = imouto.gameObject.GetComponents<BoxCollider2D>();
        for (int i = 0; i < boxCollider2Ds.Length; i++)
        {
            if (boxCollider2Ds[i].isTrigger)
            {
                boxCollider2Ds[i].enabled = false;
            }
        }
    }

}