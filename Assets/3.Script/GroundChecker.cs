using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGround()
    {
        CircleCollider2D coll = this.GetComponent<CircleCollider2D>();
        ContactFilter2D contactFilter = new ContactFilter2D();
        List<Collider2D> colls= new List<Collider2D>();
        coll.OverlapCollider(contactFilter, colls);

        foreach (Collider2D target in colls)
        {
            if (target.tag == "ground")
            {
                return true;
            }
        }
        return false;
    }
}
