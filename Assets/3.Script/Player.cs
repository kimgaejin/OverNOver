using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Player playerScript;
    private GameObject playerGameObject;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    private enum State { DEFAULT, RUN, ATTACKING, ATTACKED };
    private State curState;

    private void Awake()
    {
        playerGameObject = this.gameObject;
        playerMovement = playerGameObject.GetComponent<PlayerMovement>();
        playerAttack = playerGameObject.GetComponent<PlayerAttack>();
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
            if (playerAttack.IsAttackStarting())
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

}
