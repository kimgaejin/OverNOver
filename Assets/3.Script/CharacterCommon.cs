using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommon : MonoBehaviour
{
    protected Health health;

    protected virtual void Init()
    {
        health = this.gameObject.GetComponent<Health>();
    }
}
