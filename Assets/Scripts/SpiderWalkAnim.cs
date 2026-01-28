using System.Collections;
using UnityEngine;

public class SpiderWalkAnim : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float stepDestunce = 1.0f;
    [SerializeField] private float stepHeight = 0.5f;
    [SerializeField] private float delayBetweenSteps = 0.2f;



    [SerializeField] private Transform[] legsIK_Targets;


    private Vector3 lastPos;
    private Vector3 currentPos;

    private void Start()
    {
        lastPos = transform.position;
        currentPos = transform.position;
    }

    private void Update()
    {
        
        CheckBodyDistance();

    }


    private void CheckBodyDistance()
    {
        currentPos = transform.position;
        float distance = Vector3.Distance(currentPos, lastPos);
        if (distance > stepDestunce)
        {
            StartCoroutine(MoveLegsToNewPosition());
            lastPos = currentPos;
        }

    }

    Vector3[] TR_Gizmo;
    private IEnumerator MoveLegsToNewPosition()
    {
        if (TR_Gizmo == null)
        {

        TR_Gizmo = new Vector3[legsIK_Targets.Length];
        }
        int index = 0;
        foreach (Transform legTarget in legsIK_Targets)
        {
            Vector3 initialPos = legTarget.position;
            Vector3 targetPos =  FindNewStepPosition(initialPos); // Simplified target position calculation

            TR_Gizmo[index] = targetPos;
            index++;

            float elapsedTime = 0f;
            while (elapsedTime < delayBetweenSteps)
            {
                float t = elapsedTime / delayBetweenSteps;
                // Move leg in an arc
                float heightOffset = Mathf.Sin(t * Mathf.PI) * stepHeight;
                legTarget.position = Vector3.Lerp(initialPos, targetPos, t) + new Vector3(0, heightOffset, 0);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            legTarget.position = targetPos; // Ensure final position is set
        }
    }
    private Vector3 FindNewStepPosition(Vector3 newPosition)
    {
        // This method can be expanded to include logic for finding a valid step position
        Ray ray = new Ray(newPosition + Vector3.up * 2, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 5f))
        {
            newPosition = hitInfo.point;
        }
        return newPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (TR_Gizmo != null)
        {
            foreach (Vector3 pos in TR_Gizmo)
            {
                Gizmos.DrawSphere(pos, 0.4f);
            }
        }
    }

}
