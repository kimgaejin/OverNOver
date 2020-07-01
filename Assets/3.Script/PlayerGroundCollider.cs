using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollider : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private bool isGround;
    private bool isJumpGround;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ground" || collision.tag == "object")
        {
            isGround = true;
        } else if (collision.tag == "jumpGround")
        {
            isJumpGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ground" || collision.tag == "object")
        {
            isGround = false;
        } else if (collision.tag == "jumpGround")
        {
            isJumpGround = false;
        }
    }

    public void SetPlayerMovement(PlayerMovement playerMovement)
    {
        this.playerMovement = playerMovement;
    }

    public bool GetIsGround()
    {
        return isGround;
    }

    public bool GetIsJumpGround()
    {
        return isJumpGround;
    }
}
