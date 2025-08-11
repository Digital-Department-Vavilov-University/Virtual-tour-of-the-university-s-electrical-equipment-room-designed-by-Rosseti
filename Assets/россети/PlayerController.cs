
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Скорость движения персонажа
    public float mouseSensitivity = 2.0f; // Чувствительность мыши
    public Transform playerCamera; // Ссылка на трансформ камеры

    private CharacterController controller;
    private float verticalRotation = 0f; // Текущий угол вертикального поворота

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        // === Обработка поворота камеры ===
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Вертикальный поворот (ограниченный)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Горизонтальный поворот персонажа
        transform.Rotate(Vector3.up * mouseX);

        // === Обработка движения ===
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Направление движения относительно текущего поворота
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // Применяем гравитацию
        moveDirection.y -= 9.81f * Time.deltaTime;

        // Двигаем персонажа
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
    private Laboratory lab;
    private void OnTriggerStay(Collider other)
    {
        if(lab == null)
            lab = other.GetComponent<Laboratory>();
        if (lab != null)
            lab.inTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if(lab != null)
            lab.inTrigger = false;
    }
}