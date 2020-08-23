using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : SkillObject
{
    private GameObject target;
    private GameObject bullet;
    private GroundChecker groundChecker;

    private void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        skillEffects.Add(new Damage(20.0f, false));
        skillEffects.Add(new KnockBack(this.gameObject, 8.0f));

        groundChecker = transform.Find("GroundChecker").GetComponent<GroundChecker>();
        bullet = transform.Find("Graphics").Find("Bullet").gameObject;

        bullet.SetActive(false);
        state = State.PREPARE;
    }

    public override void Spell(GameObject target)
    {
        base.Spell(target);

        this.target = target;
        groundChecker.transform.position = target.transform.position;
    }

    private void Update()
    {
        if (state == State.PREPARE)
        {
            if (groundChecker.IsGround() == true)
            {
                bullet.SetActive(true);
                bullet.transform.position = groundChecker.transform.position += new Vector3(10, 10, 0);
                state = State.SPELL;
            }
            else
            {
                groundChecker.transform.position += Vector3.down * Time.deltaTime;
            }
        }
        else if (state == State.SPELL)
        {
            bullet.transform.position += new Vector3(-3, -3, 0) * Time.deltaTime;
            groundChecker.transform.position = bullet.transform.position;
            if (groundChecker.IsGround() == true)
            {
                SetActive(false);
            }
        }
    }
}
