using UnityEngine;
using System.Collections.Generic;
public class SpiderBoddyController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float boddyHeightOffset = 1.0f;
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
        Vector3 newPosition = rb.position + moveInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
    
}
