using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect
{
    public virtual void Do(GameObject target)
    {

    }
}
public class KnockBack : SkillEffect
{
    GameObject from;
    GameObject target;
    Vector2 arrow;
    float power;

    public KnockBack(GameObject from, float power)
    {
        this.from = from;
        this.power = power;
    }

    public override void Do(GameObject target)
    {
        Rigidbody2D rigid = target.GetComponent<Rigidbody2D>();

        if (!rigid) return;

        rigid.AddForce((target.transform.position - from.transform.position).normalized * power, ForceMode2D.Impulse);
    }
}
