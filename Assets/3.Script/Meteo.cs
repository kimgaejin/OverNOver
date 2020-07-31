using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : SkillObject
{
    private void Start()
    {
        Init();
        skillEffects.Add(new Damage(10.0f, false));
        skillEffects.Add(new KnockBack(this.gameObject, 2.0f));
    }

    private void Update()
    {
        transform.position += new Vector3(-0.1f, -0.1f, 0);
    }
}
