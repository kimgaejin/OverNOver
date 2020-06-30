﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWarrior : MonsterCommon
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
        Property("SkullWarrior", 100.0f, 2.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Routine();
    }

    protected override void AttackPre()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log("attackPreing");

        if (stateInfo.IsName(monsterName + "_attackPre") && 0.99f <= stateInfo.normalizedTime)
        {
            state = State.ATTACKING;
            animator.SetBool("attackPre", false);
            animator.SetBool("attack", true);

            // 플레이어를 향해 대쉬
            Vector2 distance = (Vector2)(player.transform.position - this.transform.position).normalized;
            rigid.AddForce(distance * speed * speed, ForceMode2D.Impulse);
        }

    }
}