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
    protected Animator animator;
    protected Collider2D searchRange;
    protected State state;
    protected float hp;
    protected float speed = 2.0f;

    protected float curTimer;
    protected float recTimer; 

    protected virtual void Init()
    {
        searchRange = this.gameObject.transform.Find("SearchRange").GetComponent<Collider2D>();
        animator = this.gameObject.transform.Find("Graphics").GetComponent<Animator>();
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
        Debug.Log("chasing " + (player.transform.position - this.transform.position).magnitude.ToString());
        if ((player.transform.position - this.transform.position).magnitude < 2.0f)
        {
            state = State.ATTACKPRE;
            animator.SetBool("attackPre", true);
        }
    }

    protected virtual void AttackPre()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log("attackPreing");

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
