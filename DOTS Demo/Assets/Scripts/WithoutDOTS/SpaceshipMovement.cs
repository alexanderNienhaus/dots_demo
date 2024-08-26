using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10000;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Random.insideUnitCircle.normalized * moveSpeed * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);
        }
    }
}
