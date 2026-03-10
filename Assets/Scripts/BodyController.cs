using UnityEngine;

 public enum GaitGroup { A, B }
public class BodyController : MonoBehaviour
{

    [SerializeField] LegController[] legControllers;
    public float heightOffset = 1;
    public float alignSpeed = 5f;
    public float moveSpeed = 5f;
    public float rotSpeed = 5f;

     // 0 = Group A stepping, 1 = Group B stepping
    private int activeGaitGroup = 0;
    private float gaitTimer = 0f;
    [SerializeField] float gaitSwitchInterval = 0.3f; // tune this
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AlignPos();
        AlignRot();
        Move();
        GaiteMonitor();
    }

    private void Move()
    {
        float inputY = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * inputY * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * inputX * rotSpeed * Time.deltaTime);
    }
    private void AlignPos()
    {
        // Body height = average of all foot positions
        Vector3 avgPos = Vector3.zero;
        foreach (var leg in legControllers)
            avgPos += leg.GetPosition();
        avgPos /= legControllers.Length;

        // Move body to that height
        Vector3 target = transform.position;
        target.y = avgPos.y + heightOffset;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * alignSpeed);
    }

    private void AlignRot()
    {
        // Average all surface normals from raycasts
        Vector3 avgNormal = Vector3.zero;
        foreach (var leg in legControllers)
            avgNormal += leg.GetHitPoint().normal;
        avgNormal = (avgNormal / legControllers.Length).normalized;

        // Build rotation: up = surface normal, forward = where spider faces
        Vector3 right = Vector3.Cross(avgNormal, transform.forward).normalized;
        Vector3 correctedForward = Vector3.Cross(right, avgNormal).normalized;
        Quaternion targetRot = Quaternion.LookRotation(correctedForward, avgNormal);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * alignSpeed);

    }

    private void GaiteMonitor()
    {
         gaitTimer += Time.deltaTime;
        if (gaitTimer >= gaitSwitchInterval)
        {
            gaitTimer = 0f;
            activeGaitGroup = 1 - activeGaitGroup; // toggle 0↔1
        }
    }
    public bool IsGroupAllowedToStep(GaitGroup group)
    {
        return (int)group == activeGaitGroup;
    }
}
