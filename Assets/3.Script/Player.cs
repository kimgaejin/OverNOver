using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Player playerScript;
    private GameObject playerObject;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    private enum State { DEFAULT, RUN, ATTACKING, ATTACKED };
    private State curState;

    private UnityEngine.Quaternion leftFace;
    private UnityEngine.Quaternion rightFace;


    private void Awake()
    {
        playerObject = this.gameObject;
        playerScript = playerObject.GetComponent<Player>();
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        playerAttack = playerObject.GetComponent<PlayerAttack>();

        leftFace.eulerAngles = new Vector3(0, -180, 0);
        rightFace.eulerAngles = new Vector3(0, 0, 0);

        playerMovement.Init();
        playerAttack.Init();
    }

    private void Update()
    {
        Debug.Log("Player Cur State: " + curState.ToString());
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

    }

    public void FlipToRight(bool isFaceRight)
    {
        if (isFaceRight == true) playerObject.transform.rotation = rightFace;
        else playerObject.transform.rotation = leftFace;
    }


}
