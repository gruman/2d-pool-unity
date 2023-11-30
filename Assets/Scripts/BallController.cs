using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float power = 10f;
    public Rigidbody2D rb;
    public Vector2 minPower;
    public Vector2 maxPower;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;
    bool isOverBall = false;

    LineController line;

    // Start is called before the first frame update
    private void Awake()
    {
        line = GetComponent<LineController>();
    }

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOverBall)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    startPoint = cam.ScreenToWorldPoint(touch.position);
                    startPoint.z = 15;
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    Vector3 currentPoint = cam.ScreenToWorldPoint(touch.position);
                    line.RenderLine(startPoint, currentPoint);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    endPoint = cam.ScreenToWorldPoint(touch.position);
                    endPoint.z = 15;

                    ApplyForce();
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                line.RenderLine(startPoint, currentPoint);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                ApplyForce();
            }
        }
    }

    private void OnMouseDown()
    {
        isOverBall = true;
    }

    private void OnMouseUp()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        endPoint.z = 15;

        ApplyForce();
    }

    void ApplyForce()
    {
        force = new Vector2(
            Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
            Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y)
        );

        rb.AddForce(force * power, ForceMode2D.Impulse);
        line.EndLine();
        isOverBall = false;
    }
}
