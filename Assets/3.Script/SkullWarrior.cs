using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWarrior : MonsterCommon
{
    void Start()
    {
        Init();
    
    }

    protected override void Init()
    {
        base.Init();

        Property("SkullWarrior", 100.0f, 2.0f, 0.5f);
        skillEffects.Add(new KnockBack(this.gameObject, 3.0f));
        skillEffects.Add(new Damage(25.0f, true));
        health.Init(this.gameObject, 100, "Graphics", new string[] { "playerAttack" });

    }

    void Update()
    {
        Routine();
    }

    protected override void AttackPre()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(monsterName + "_attackPre") && 0.99f <= stateInfo.normalizedTime)
        {
            state = State.ATTACKING;
            animator.SetBool("attackPre", false);
            animator.SetBool("attack", true);

            // 플레이어가 있던 장소를 향해 대쉬
            Vector2 distance = new Vector2(targetPosition.x - this.transform.position.x, 0).normalized;
            rigid.AddForce(distance * speed * 1, ForceMode2D.Impulse);
        }
    }
}
