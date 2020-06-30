using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    GameObject playerObject;
    Animator playerAttackAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        playerObject = this.gameObject;
        playerAttackAnimator = playerObject.transform.Find("SwordGraphics").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            playerAttackAnimator.SetBool("isAttack", true);
        }
        else
        {
            playerAttackAnimator.SetBool("isAttack", false);

        }
    }
}
