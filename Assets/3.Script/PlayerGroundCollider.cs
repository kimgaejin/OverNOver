using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollider : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private bool isGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            playerMovement.IsOnEnemy();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ground" || collision.tag == "object")
        {
            isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ground" || collision.tag == "object")
        {
            isGround = false;
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
}
