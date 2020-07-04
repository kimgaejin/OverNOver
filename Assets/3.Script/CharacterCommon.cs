using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommon : MonoBehaviour
{
    protected Health health;
    protected List<SkillEffect> skillEffects;


    protected virtual void Init()
    {
        health = this.gameObject.GetComponent<Health>();

        skillEffects = new List<SkillEffect>();
    }

    public virtual void Dead()
    {

    }

    public List<SkillEffect> GetSkillEffects()
    {
        return skillEffects;
    }
}
