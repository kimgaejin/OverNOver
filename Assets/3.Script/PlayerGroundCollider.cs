using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollider : MonoBehaviour
{
    private bool isGround;

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

    public bool GetIsGround()
    {
        return isGround;
    }
}
