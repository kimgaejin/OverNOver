using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class Player : CharacterCommon
{
    private Player playerScript;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private Health playerHealth;

    private enum State { DEFAULT, RUN, ATTACKING, ATTACKED };
    private State curState;

    private float curTimer;

    private UnityEngine.Quaternion leftFace;
    private UnityEngine.Quaternion rightFace;


    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        skillEffects.Add(new Damage(10.0f, false));
        skillEffects.Add(new KnockBack(thisObject, 2.0f));

        thisObject = this.gameObject;
        playerScript = thisObject.GetComponent<Player>();
        playerMovement = thisObject.GetComponent<PlayerMovement>();
        playerAttack = thisObject.GetComponent<PlayerAttack>();
        playerHealth = thisObject.GetComponent<Health>();

        leftFace.eulerAngles = new Vector3(0, -180, 0);
        rightFace.eulerAngles = new Vector3(0, 0, 0);

        playerMovement.Init();
        playerAttack.Init();
        playerHealth.Init(thisObject, 100, "BodyGraphics", new string[] { "enemyAttack" });
    }

    private void Update()
    {
        if (curState == State.DEFAULT)
        {
            curState = State.RUN;
        }
        else if (curState == State.RUN)
        {
            playerMovement.Move();
            if (playerAttack.IsAttackStarting(playerMovement.IsRightFace()))
            {
                curState = State.ATTACKING;
            }
        }
        else if (curState == State.ATTACKING)
        {
            if (playerAttack.Attack())
            {

            }
            else
            {
                curState = State.RUN;
            }
        }
        else if (curState == State.ATTACKED)
        {
            curState = State.RUN;
        }

        curTimer += Time.deltaTime;
    }

    public override void Interrupt(string name)
    {
        if (name == "damaged")
        {
            curState = State.ATTACKED;
            playerAttack.AttackFail();
            ParticleManager.Instance.Show(Particle.SpriteType.BLOOD, transform.position);
        }
    }

    public void FlipToRight(bool isFaceRight)
    {
        if (isFaceRight == true) thisObject.transform.rotation = rightFace;
        else thisObject.transform.rotation = leftFace;
    }

    public float GetTimer()
    {
        return curTimer;
    }
}
