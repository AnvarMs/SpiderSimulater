using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public class SpiderBoddyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float heightLearpSpeed = 5f;
    [SerializeField] float rotationSlearpSpeed = 5f;
    [SerializeField] Vector3 boddyHeightOffset;
    [SerializeField] List<SpiderLegController> legControllers;
    Rigidbody rb;
    Vector3 moveInput = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("PlayerController requires a Rigidbody component on the same GameObject.");
            return;
        }

        // Prevent the physics from rotating the player (common for character controllers)
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Smoother movement visual when camera/objects move; optional but helpful
        rb.interpolation = RigidbodyInterpolation.Interpolate;

      

    }

    void Update()
    {
       MoveSpider();
        // Update boddy height based on leg positions
        AlligneBody();
    }

    private void AlligneBody()
    {
        if (legControllers.Count == 0) return;
        int groundedLegs = 0;
        Vector3 averageLegPosition = Vector3.zero;
        Vector3 averageNormal = Vector3.zero;
        foreach (var leg in legControllers)
        {
            if (!leg.IsGrounded) continue;

            averageLegPosition += leg.GetPosition();
            averageNormal += leg.GetHitPoint().normal;
            groundedLegs++;
        }
        if (groundedLegs < 2)
            return;
        averageLegPosition /= groundedLegs;
        averageNormal /= groundedLegs;
        Quaternion rotationFromUpToNormal = Quaternion.FromToRotation(transform.up, averageNormal);
        Vector3 targetBoddyPosition = new Vector3(transform.position.x, averageLegPosition.y, transform.position.z) + boddyHeightOffset;

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationFromUpToNormal * transform.rotation, Time.deltaTime * rotationSlearpSpeed);
        transform.position = Vector3.Lerp(transform.position, targetBoddyPosition, Time.deltaTime * heightLearpSpeed);
    }

    private void MoveSpider()
    {
        if (rb == null) return;

        // Read input in Update so we don't miss input between physics frames
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Build movement vector in local space
        moveInput = transform.right * horizontal + transform.forward * vertical;

        // Prevent faster diagonal movement
        if (moveInput.sqrMagnitude > 1f)
        {
            moveInput.Normalize();
        }

        // Move using Rigidbody.MovePosition to respect physics & collisions
        Vector3 newPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
    
}
