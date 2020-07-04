using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommon : MonoBehaviour
{
    protected GameObject thisObject;
    protected Health health;
    protected List<SkillEffect> skillEffects;
    protected Rigidbody2D rigid;



    protected virtual void Init()
    {
        thisObject = this.gameObject;
        health = this.gameObject.GetComponent<Health>();
        rigid = this.gameObject.GetComponent<Rigidbody2D>();

        skillEffects = new List<SkillEffect>();
    }

    public virtual void Dead()
    {

    }

    public virtual void Interrupt(string name)
    {

    }

    public List<SkillEffect> GetSkillEffects()
    {
        return skillEffects;
    }
}
