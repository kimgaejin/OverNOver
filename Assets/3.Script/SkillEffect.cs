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

        Vector2 arrow = (Vector2)(target.transform.position - from.transform.position);
        Vector2 dest = Vector2.zero;
        
        if (0 <= arrow.x) dest.x = 1;
        else dest.x = -1;
        dest.y = 0.5f;

        rigid.AddForce(dest * power, ForceMode2D.Impulse);
    }
}

public class Damage : SkillEffect
{
    float damage;
    bool invincibilityType;

    public Damage(float damage, bool invincibilityType)
    {
        this.damage = damage;
        this.invincibilityType = invincibilityType;
    }

    public override void Do(GameObject target)
    {
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth)
        {
            targetHealth.Damaged(damage, invincibilityType);
        }
    }
}