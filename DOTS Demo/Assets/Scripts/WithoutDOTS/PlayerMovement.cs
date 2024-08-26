using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10000f;
    [SerializeField] private float speedH = 2f;
    [SerializeField] private float speedV = 2f;

    private Rigidbody rb;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float timeMoveSpeed = moveSpeed * Time.deltaTime;

        float horizontalAxis = 0;
        float verticalAxis = 0;
        if (Input.GetKey(KeyCode.W))
        {
            verticalAxis = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verticalAxis = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalAxis = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalAxis = 1;
        }
        Vector3 right = Camera.main.transform.right;
        Vector3 forward = Camera.main.transform.forward;
        right.y = 0f;
        forward.y = 0f;
        right.Normalize();
        forward.Normalize();
        Vector3 desiredMoveDirection = right * horizontalAxis + forward * verticalAxis;
        rb.velocity = desiredMoveDirection * timeMoveSpeed;

        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = Vector3.up * timeMoveSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = Vector3.down * timeMoveSpeed;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            Camera.main.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}
