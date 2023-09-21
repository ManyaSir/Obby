using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 5f;
    public float mass = 1f;
    public float gravity = 9.8f;
    public KeyCode Jump;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        CharacterController characterController = GetComponent<CharacterController>();

        if (characterController != null)
        {
            // Используйте characterController здесь
        }
        else
        {
            Debug.LogError("CharacterController не найден на объекте");
        }
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float speed = moveSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed *= sprintMultiplier;
            }

            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        // Прыжок
        if (Input.GetKey(Jump) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Падение с учетом массы и силы тяжести
        velocity.y -= gravity * mass * Time.deltaTime;

        // Проверка на нахождение на земле
        isGrounded = controller.isGrounded;

        // Применяем гравитацию и движение
        controller.Move(velocity * Time.deltaTime);
    }
}