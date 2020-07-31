using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    protected List<SkillEffect> skillEffects;

    protected void Init()
    {
        skillEffects = new List<SkillEffect>();
    }

    public List<SkillEffect> GetSkillEffects()
    {
        return skillEffects;
    }
}
