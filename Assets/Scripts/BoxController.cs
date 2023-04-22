using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour
{

    public Camera cam;

    public GameController gameController;
    private float maxWidth;

    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float boxWidth = GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - boxWidth;
    }

    // Update is called once per physics timestep
    void FixedUpdate()
    {
        if(gameController.timeLeft > 0) {
            Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = new Vector3(rawPosition.x, 0.0f, 0.0f);
            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
            GetComponent<Rigidbody2D>().MovePosition(targetPosition);
        }
    }
}
