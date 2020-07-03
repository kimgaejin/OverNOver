using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCommon : CharacterCommon
{
    protected enum State { DEFAULT, SEARCHING, CHASING, ATTACKPRE, ATTACKING, ATTACKED, DEAD };

    // refers
    protected GameObject player;
    protected Vector3 targetPosition;
    protected Rigidbody2D rigid;
    protected Animator animator;
    protected Collider2D searchRange;

    // property
    protected string monsterName;
    protected float hp;
    protected float speed = 2.0f;
    protected float attackRange = 1.0f;
    protected float damage;
    protected List<SkillEffect> skillEffects;

    // states
    protected State state;
    protected float curTimer;
    protected float recTimer;

    // const
    protected Quaternion leftFace;
    protected Quaternion rightFace; // 기본

    protected virtual void Init()
    {
        base.Init();

        rightFace = Quaternion.Euler(0, 0, 0);
        leftFace = Quaternion.Euler(0, -180, 0);

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

        skillEffects = new List<SkillEffect>();
    }

    protected virtual void Routine()
    {
        curTimer += Time.deltaTime;

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
            Rotate();
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
            // 사정거리 내에 진입하더라도, 해당 프레임에 50% 확률로 플레이어를 공격하지 않는다.
            // 이를 통해 동시에 접근한 몬스터들이 텀을 둬서 시간차로 공격하는 것을 노립니다.
            float r = Random.value;
            if (0.5f <= r)
            {
                state = State.ATTACKPRE;
                animator.SetBool("attackPre", true);
                targetPosition = player.transform.position;
            }
        }
    }

    protected virtual void AttackPre()
    {
        Rotate();
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

    protected virtual void Rotate()
    {
        if (!player) return;
        if (player.transform.position.x >= this.transform.position.x) this.transform.rotation = rightFace;
        else this.transform.rotation = leftFace;
    }

    public List<SkillEffect> GetSkillEffects()
    {
        return skillEffects;
    }
}
