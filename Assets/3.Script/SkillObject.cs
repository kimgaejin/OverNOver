using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    protected enum State { DEFAULT, PREPARE, CHANELING, SPELL, AFTER, DEAD };
    protected State state;
    protected List<SkillEffect> skillEffects;

    protected virtual void Init()
    {
        skillEffects = new List<SkillEffect>();
    }

    public virtual void Spell(GameObject target)
    {

    }

    public List<SkillEffect> GetSkillEffects()
    {
        return skillEffects;
    }
}
