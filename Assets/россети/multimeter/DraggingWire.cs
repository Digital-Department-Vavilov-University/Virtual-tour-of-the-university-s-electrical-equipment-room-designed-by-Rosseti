using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class DraggingWire : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [Header("Drag Settings")]
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float maxRadius = 5f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 100f; // Скорость вращения колесиком
    [SerializeField] private float yRotationOffset = 0f; // Начальное смещение вращения

    [Header("Collision Settings")]
    [SerializeField] private string forbiddenTag = "Multimeter";
    [SerializeField] private Vector3 collisionCheckBox = new Vector3(0.1f,0.1f,0.1f);

    private Vector3 offset;
    private float zCoordinate;
    private Rigidbody rb;
    private bool isDragging;
    public bool IsDragging=> isDragging;
    private float initialY;
    private Vector3 circleCenter;
    private Vector3 lastSafePosition;
    private float currentRotationY; // Текущий угол вращения по Y

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (centerPoint != null)
        {
            circleCenter = centerPoint.position;
        }
        else
        {
            circleCenter = transform.position;
            circleCenter.y = 0;

            centerPoint = new GameObject("DragCenter").transform;
            centerPoint.position = circleCenter;
        }

        initialY = transform.position.y;
        lastSafePosition = transform.position;

        // Инициализация вращения
        currentRotationY = transform.eulerAngles.y + yRotationOffset;
        ApplyRotation();
    }

    void OnMouseDown()
    {
        StartDragging();
    }

    void OnMouseUp()
    {
        StopDragging();
    }

    void Update()
    {
        if (isDragging)
        {
            // Обработка вращения колесиком мыши
            HandleRotationInput();
            DragObject();
        }
    }

    private void HandleRotationInput()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            currentRotationY += scroll * rotationSpeed * Time.deltaTime;
            ApplyRotation();
        }
    }

    private void ApplyRotation()
    {
        // Устанавливаем вращение только по оси Y
        transform.rotation = Quaternion.Euler(0, currentRotationY, 0);
    }

    private void StartDragging()
    {
        isDragging = true;
        rb.useGravity = false;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.freezeRotation = true; // Фиксируем физическое вращение

        zCoordinate = cam.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();

        initialY = transform.position.y;
        lastSafePosition = transform.position;
    }

    private void DragObject()
    {
        Vector3 targetPosition = GetMouseWorldPos() + offset;
        targetPosition = ApplyCircleConstraint(targetPosition);

        if (CanMoveTo(targetPosition))
        {
            rb.MovePosition(targetPosition);
            lastSafePosition = targetPosition;
        }
        else
        {
            rb.MovePosition(lastSafePosition);
        }
    }

    private bool CanMoveTo(Vector3 targetPosition)
    {
        Collider[] hitColliders = Physics.OverlapBox(targetPosition, collisionCheckBox, Quaternion.identity);

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag(forbiddenTag))
            {
                return false;
            }
        }
        return true;
    }

    private Vector3 ApplyCircleConstraint(Vector3 targetPos)
    {
        Vector3 circleToTarget = targetPos - circleCenter;
        circleToTarget.y = 0;

        if (circleToTarget.magnitude > maxRadius)
        {
            circleToTarget = circleToTarget.normalized * maxRadius;
        }

        Vector3 result = circleCenter + circleToTarget;
        result.y = initialY;
        return result;
    }

    private void StopDragging()
    {
        isDragging = false;

        Vector3 toObject = transform.position - circleCenter;
        toObject.y = 0;
        if (toObject.magnitude > maxRadius)
        {
            Vector3 newPosition = circleCenter + toObject.normalized * maxRadius;
            newPosition.y = initialY;
            rb.MovePosition(newPosition);
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.freezeRotation = false;
        rb.useGravity = true;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoordinate;
        return cam.ScreenToWorldPoint(mousePoint);
    }

}