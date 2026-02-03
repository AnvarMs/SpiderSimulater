
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SpiderLegController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform boddyTrasform;
    Transform footIkTarget;
    [SerializeField] private SpiderLegController pairedLeg;

    [Header("Settings")]
    [SerializeField] float stepThreshold = 2;
    [SerializeField] float stepDistance = 1;
    [SerializeField] float stepHeight = 0.7f;
    [SerializeField] float stepSpeed = 10;
    [SerializeField] float raycastHeightOffset = 2;

    // Private variables
    private Vector3 defaultLocalOffset;
    private Vector3 FootPose;
    private Vector3 desairedFootPose;
    private Vector3 lastBoadyPos;
    private Vector3 bodyVelocity;
    private float maxLegLength;
    // Raycasting variables
    private Vector3 rayOrigin;
    private RaycastHit hitPoint;

    //animation variables
    [SerializeField] private bool isSteping = false;

    // Public properties
    public bool IsGrounded { get { return !isSteping; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (footIkTarget == null)
        {
            footIkTarget = transform;
        }
        defaultLocalOffset = boddyTrasform.InverseTransformPoint(footIkTarget.position);
        FootPose = footIkTarget.position;
        lastBoadyPos = boddyTrasform.position;
        maxLegLength = Vector3.Distance(boddyTrasform.position, footIkTarget.position);

    }

    // Update is called once per frame
    void Update()
    {
        bodyVelocity = (boddyTrasform.position - lastBoadyPos) / Time.deltaTime;
        lastBoadyPos = boddyTrasform.position;

        Vector3 moveDirection = bodyVelocity.normalized;
        float speed = bodyVelocity.magnitude;

        Vector3 velocityOffset = moveDirection * speed * 0.1f;
        velocityOffset = Vector3.ClampMagnitude(velocityOffset, stepDistance);


        if (isSteping)
            return;

        Vector3 localPrediction = defaultLocalOffset + Vector3.forward * stepDistance;


        desairedFootPose = boddyTrasform.TransformPoint(localPrediction) + velocityOffset;

       Vector3 vectorFromBody = desairedFootPose - boddyTrasform.position;

        if (vectorFromBody.magnitude > maxLegLength)
            desairedFootPose = boddyTrasform.position + vectorFromBody.normalized * maxLegLength;

        if (Vector3.Distance(FootPose, desairedFootPose) > stepThreshold)
        {
            rayOrigin = desairedFootPose + Vector3.up * raycastHeightOffset;
            if (Physics.Raycast(rayOrigin, Vector3.down, out hitPoint, raycastHeightOffset * 3))
            {

                if (!isSteping && pairedLeg.IsGrounded)
                    StartCoroutine(MoveFoot());
            }
        }
    }



    private IEnumerator MoveFoot()
    {
        isSteping = true;
        Vector3 startpos = footIkTarget.position;
        Vector3 endPos = hitPoint.point;
        float t = 0;
        while (t < 1)
        {
            t = Mathf.Clamp01(t + stepSpeed * Time.deltaTime);
            Vector3 targetPos = Vector3.Lerp(startpos, endPos, t);
            targetPos = Vector3.up * stepHeight * Mathf.Sin(t * Mathf.PI) + targetPos;
            footIkTarget.position = targetPos;
            yield return null;
        }
        FootPose = footIkTarget.position;
        isSteping = false;
    }

    public Vector3 GetPosition()
    {
        return FootPose;
    }
    public RaycastHit GetHitPoint()
    {
        return hitPoint;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(desairedFootPose, 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * raycastHeightOffset * 2);
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(FootPose, 0.3f);

    }
}
