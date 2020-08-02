using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private bool isGround;

    private void OnTriggerEnter2D(Collider2D collsion)
    {
        if (collsion.tag == "ground")
        {
            isGround = true;
        }
    }

    private void Update()
    {
        if (!isGround)
        {
            this.transform.position += Vector3.down * Time.deltaTime;
        }
    }

    public bool IsGround()
    {
        return isGround;
    }
}
