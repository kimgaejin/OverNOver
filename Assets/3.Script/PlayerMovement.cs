using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject playerObject;
    private Rigidbody2D rigidbody2d;

    private float moveSpeed = 1.0f;
    private float jumpPower = 3.0f;

    private void Start()
    {
        playerObject = this.gameObject;
        rigidbody2d = playerObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // move
        Vector3 arrow = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) arrow += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow)) arrow += Vector3.right;
        arrow = arrow.normalized * moveSpeed * Time.deltaTime;
        playerObject.transform.Translate(arrow, Space.World);

        // jump
        if (Input.GetKeyDown(KeyCode.S)) 
            rigidbody2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}
