using System.Collections;
using UnityEngine;

public class LegController : MonoBehaviour
{

    [SerializeField] Transform body;
    [SerializeField] LegController pairedLeg;
    public LayerMask groundLayer;
    public float maxDestence =2f;
    public float rayCastDist = 2f;
    public float stepSpeed=1f;
    public float stepHeight = 1f;
    Vector3 desiredFootPos;
    RaycastHit raycast;

    bool isStepping =false;

    public bool IsGrounded => !isStepping;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        desiredFootPos = body.InverseTransformPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLeg();
    }

    private void UpdateLeg()
    {
        Vector3 desiredPos = body.TransformPoint(desiredFootPos);
        var dist = Vector3.Distance(desiredPos,transform.position);

        if( dist > maxDestence)
        {
            Vector3 rayStart =  desiredPos + Vector3.up*5f;
            if (Physics.Raycast(rayStart, Vector3.down, out raycast, 10f, groundLayer))
            {
                Debug.DrawRay(rayStart, Vector3.down* raycast.distance, Color.red);
                if(!isStepping && pairedLeg.IsGrounded)
                StartCoroutine(MoveFoot(transform.position,raycast.point));
            }
        }
       
    }

    IEnumerator MoveFoot(Vector3 startPos,Vector3 endPos)
    {
        float t=0;
        isStepping = true;
        while (t < 1)
        {
            t += stepSpeed * Time.deltaTime;

            Vector3 pos = Vector3.Lerp(startPos,endPos,t);
            pos.y += stepHeight * Mathf.Sin(t * Mathf.PI);
            transform.position = pos;
            yield return null; 
        }

        isStepping = false;
    }


     
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public RaycastHit GetHitPoint()
    {
        return raycast;
    }

}
