using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform racket;
    public Transform ballSpawnPoint;
    public LineRenderer lineRenderer;
    public float maxPullDistance = 5f;
    public float launchForce = 10f;
    
    private Vector3 initialPosition;
    private bool isAiming;
    private GameObject currentBall;
    private Rigidbody ballRigidbody;

    void Start()
    {
        initialPosition = racket.position;
        SpawnBall(); // Create the first ball
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;
        }
        
        if (Input.GetMouseButton(0) && isAiming)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 aimDirection = (mousePosition - initialPosition);
            float distance = Mathf.Clamp(aimDirection.magnitude, 0, maxPullDistance);
            aimDirection = aimDirection.normalized * distance;
            
            racket.position = initialPosition + aimDirection;
            ballSpawnPoint.position = racket.position + new Vector3(0, 0, -1);

            DrawTrajectory();
        }

        if (Input.GetMouseButtonUp(0) && isAiming)
        {
            isAiming = false;
            LaunchBall();
            ResetRacket();
        }
    }

    void SpawnBall()
    {
        // Check if there is already a ball before creating a new one
        if (currentBall != null)
        {
            Destroy(currentBall);
        }
        
        currentBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
        ballRigidbody = currentBall.GetComponent<Rigidbody>();
        ballRigidbody.isKinematic = true;
    }

    void LaunchBall()
    {
        ballRigidbody.isKinematic = false;
        ballRigidbody.AddForce((ballSpawnPoint.position - racket.position) * launchForce, ForceMode.Impulse);
    }

    void ResetRacket()
    {
        racket.position = initialPosition;
        SpawnBall(); // Re-spawn the ball only if necessary
    }

    void DrawTrajectory()
    {
        // To be implemented in the next step
    }
}
