using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class Player : CharacterCommon
{
    private Player playerScript;
    private GameObject playerObject;
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
        skillEffects.Add(new KnockBack(this.gameObject, 1.0f));

        playerObject = this.gameObject;
        playerScript = playerObject.GetComponent<Player>();
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        playerAttack = playerObject.GetComponent<PlayerAttack>();
        playerHealth = playerObject.GetComponent<Health>();

        leftFace.eulerAngles = new Vector3(0, -180, 0);
        rightFace.eulerAngles = new Vector3(0, 0, 0);

        playerMovement.Init();
        playerAttack.Init();
        playerHealth.Init(playerObject, 100, "BodyGraphics", new string[] { "enemyAttack" });
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

        }

        curTimer += Time.deltaTime;
    }

    public void FlipToRight(bool isFaceRight)
    {
        if (isFaceRight == true) playerObject.transform.rotation = rightFace;
        else playerObject.transform.rotation = leftFace;
    }

    public float GetTimer()
    {
        return curTimer;
    }
}
