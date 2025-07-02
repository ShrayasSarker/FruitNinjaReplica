using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject bladeTrailPrefab; // Prefab for the blade trail
    GameObject CurrentBladeTrail; // Instance of the blade trail
    bool isCutting = false;
    Rigidbody2D rb;
    Camera cam;

    CircleCollider2D circleCollider;
    Vector2 previousPosition;
    public float minCuttingVelocity = 5f; // Minimum velocity to consider cutting

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();// Get the Rigidbody2D component
        cam = Camera.main;// Get the main camera
        circleCollider = GetComponent<CircleCollider2D>();// Get the CircleCollider2D component
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }
        if (isCutting) // If the player is cutting
        {
            UpdateCut();
        }
    }
    void StartCutting()
    {
        /*isCutting = true;
        CurrentBladeTrail = Instantiate(bladeTrailPrefab, transform); // Instantiate the blade trail prefab
        circleCollider.enabled = false;
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition); // Set the initial position of the blade*/
        //isCutting = true;

        // Snap blade position to mouse immediately

        isCutting = true;

        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);

        // Force transform position BEFORE instantiating the trail
        transform.position = newPosition;
        rb.position = newPosition;
        previousPosition = newPosition;

        // Instantiate the trail
        CurrentBladeTrail = Instantiate(bladeTrailPrefab, transform);

        // Clear old trail positions
        TrailRenderer trail = CurrentBladeTrail.GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.Clear();
        }

        // Start with collider disabled (FOR VELOCITY CHECK)
        //circleCollider.enabled = false;
        //previousPosition = cam.ScreenToWorldPoint(Input.mousePosition);


        // Immediately enable collider (no velocity check)
        circleCollider.enabled = true;


        
    }
    void StopCutting()
    {
        isCutting = false;
        CurrentBladeTrail.transform.SetParent(null); // Detach the blade trail from the blade
        Destroy(CurrentBladeTrail, 2f); // Destroy the blade trail after 2 seconds
        circleCollider.enabled = false;
    }
void UpdateCut()
{
    Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
    rb.position = newPosition;

        /*float velocity = (newPosition - previousPosition).magnitude / Time.deltaTime;

        if (velocity > minCuttingVelocity)
        {
            circleCollider.enabled = true;
        }
        else
        {
            circleCollider.enabled = false;
        }*/

        //// No velocity check needed anymore
        circleCollider.enabled = true;

    previousPosition = newPosition;
}

}
