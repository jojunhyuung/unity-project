using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float PlayerSpeed = 5f;
    float rotateSpeed = 200f;
    float gravity = -20f;

    float yVelocity;

    CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);

        // 🔹 중력 처리
        if (controller.isGrounded)
        {
            if (yVelocity < 0)
                yVelocity = -2f;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        // 🔹 최종 이동 벡터
        Vector3 finalMove = new Vector3(
            move.x * PlayerSpeed,
            yVelocity,
            move.z * PlayerSpeed
        );

        controller.Move(finalMove * Time.deltaTime);

        // 🔹 회전
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotateSpeed * Time.deltaTime);
        }
    }
}