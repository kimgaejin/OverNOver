using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject playerObject;
    private Rigidbody2D rigidbody2d;
    private Animator playerBodyAnimator;
    private PlayerGroundCollider playerGroundCollider;

    private float moveSpeed = 1.0f;
    private float jumpPower = 3.0f;

    private UnityEngine.Quaternion leftFace;
    private UnityEngine.Quaternion rightFace;

    private void Start()
    {
        playerObject = this.gameObject;
        rigidbody2d = playerObject.GetComponent<Rigidbody2D>();
        playerGroundCollider = playerObject.transform.Find("GroundCollider").GetComponent<PlayerGroundCollider>();
        playerBodyAnimator = playerObject.transform.Find("BodyGraphics").GetComponent<Animator>();

        leftFace.eulerAngles = new Vector3(0, -180, 0);
        rightFace.eulerAngles = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            Move();
            Rotate();
            Jump();
        }
        else
        {
            NoMove();
        }
    }

    private void Move()
    {
        Vector3 arrow = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow)) arrow += Vector3.left;
        else if (Input.GetKey(KeyCode.RightArrow)) arrow += Vector3.right;
        else
        {
            NoMove();
            return;
        }
        arrow = arrow.normalized * moveSpeed * Time.deltaTime;
        playerObject.transform.Translate(arrow, Space.World);
        playerBodyAnimator.SetBool("isRunning", true);
    }

    private void NoMove()
    {
        playerBodyAnimator.SetBool("isRunning", false);
    }

    private void Jump()
    {
        if (!Input.GetKeyDown(KeyCode.S)) return;

        bool isGround = rigidbody2d.velocity.y <= 0.01f && playerGroundCollider.GetIsGround() == true;
        if (isGround == false) return;

        rigidbody2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void Rotate()
    {

        if (Input.GetKey(KeyCode.LeftArrow)) playerObject.transform.rotation = leftFace;
        else if (Input.GetKey(KeyCode.RightArrow)) playerObject.transform.rotation = rightFace;
    }
}
