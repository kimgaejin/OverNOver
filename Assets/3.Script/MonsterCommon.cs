using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCommon : MonoBehaviour
{
    // 
    protected string monsterName;

    //
    protected GameObject player;
    protected enum State { DEFAULT, SEARCHING, CHASING, ATTACKPRE, ATTACKING, ATTACKED, DEAD };
    protected Rigidbody2D rigid;
    protected Animator animator;
    protected Collider2D searchRange;
    protected State state;
    protected float hp;
    protected float speed = 2.0f;
    protected float attackRange = 1.0f;

    protected float curTimer;
    protected float recTimer; 

    protected virtual void Init()
    {
        searchRange = this.gameObject.transform.Find("SearchRange").GetComponent<Collider2D>();
        animator = this.gameObject.transform.Find("Graphics").GetComponent<Animator>();
        rigid = this.gameObject.GetComponent<Rigidbody2D>();
    }

    protected virtual void Property(string _monsterName, float _hp, float _speed, float _attackRange)
    {
        monsterName = _monsterName;
        hp = _hp;
        speed = _speed;
        attackRange = _attackRange;
    }

    protected virtual void Routine()
    {
        curTimer += Time.deltaTime;
        Debug.Log("Cur State: " + state.ToString());

        if (state == State.DEFAULT)
        {
            state = State.SEARCHING;
        }
        else if (state == State.SEARCHING)
        {
            FindPlayer();
        }
        else if (state == State.CHASING)
        {
            ChasePlayer();
        }
        else if (state == State.ATTACKPRE)
        {
            AttackPre();
        }
        else if (state == State.ATTACKING)
        {
            Attacking();
        }
    }

    protected virtual void attacked(float damage)
    {
        hp -= damage;
        recTimer = curTimer;
    }

    protected bool FindPlayer()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        Collider2D[] colliders = new Collider2D[20];
        searchRange.OverlapCollider(contactFilter, colliders);

        foreach (Collider2D coll in colliders)
        {
            if (coll.tag == "Player")
            {
                player = coll.gameObject;
                state = State.CHASING;
                return true;
            }
        }

        return false;
    }

    protected virtual void ChasePlayer()
    {
        if (player == null)
        {
            state = State.SEARCHING;
            return;
        }

        Vector3 arrow = new Vector3 (player.transform.position.x - this.transform.position.x, 0, 0);
        arrow = arrow.normalized;
        
        this.transform.Translate(arrow * speed * Time.deltaTime, Space.World);
        if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < attackRange)
        {
            state = State.ATTACKPRE;
            animator.SetBool("attackPre", true);
        }
    }

    protected virtual void AttackPre()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(monsterName + "_attackPre") && 0.99f <= stateInfo.normalizedTime)
        {
            state = State.ATTACKING;
            animator.SetBool("attackPre", false);
            animator.SetBool("attack", true);
        }

    }

    protected virtual void Attacking()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(monsterName + "_attack") && 0.99f <= stateInfo.normalizedTime)
        {
            state = State.CHASING;
            animator.SetBool("attack", false);
        }
    }
}
