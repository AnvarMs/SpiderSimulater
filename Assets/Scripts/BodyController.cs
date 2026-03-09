using UnityEngine;

public class BodyController : MonoBehaviour
{

    [SerializeField] LegController[] legControllers;
    public float heightOffset = 1;
    public float alignSpeed = 5f;
    public float moveSpeed = 5f;
    public float rotSpeed = 5f;
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
    }

    private void Move()
    {
        float inputY = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");

        transform.Translate(transform.forward * inputY * moveSpeed * Time.deltaTime);
        transform.Rotate(transform.up * inputX * rotSpeed * Time.deltaTime);
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
}
