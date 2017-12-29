using UnityEngine;
// Script from https://forum.unity.com/threads/rts-camera-script.72045/ with modifications

public class CameraControl : MonoBehaviour {
    public float ScrollSpeed = .5f;

    public float ScrollEdge = 0.1f;

    public float PanSpeed = 10;

    public Vector2 zoomRange = new Vector2(-10, 100);

    public float CurrentZoom = 0;

    public float ZoomZpeed = 1;

    public float ZoomRotation = 1;

    public Vector2 zoomAngleRange = new Vector2(20, 70);

    public float rotateSpeed = 100;

    private Vector3 initialPosition;

    private Vector3 initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * PanSpeed, Space.Self);
        }
        else if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * -PanSpeed, Space.Self);
        }

        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * PanSpeed, Space.Self);
        }
        else if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -PanSpeed, Space.Self);
        }

        if (Input.GetKey("q"))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -rotateSpeed, Space.World);
        }
        else if (Input.GetKey("e"))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
        }

        // zoom in/out
        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomZpeed;

        CurrentZoom = Mathf.Clamp(CurrentZoom, zoomRange.x, zoomRange.y);

        transform.position = new Vector3(transform.position.x, transform.position.y - (transform.position.y - (initialPosition.y + CurrentZoom)) * 0.1f, transform.position.z);

        float x = transform.eulerAngles.x - (transform.eulerAngles.x - (initialRotation.x + CurrentZoom * ZoomRotation)) * 0.1f;
        x = Mathf.Clamp(x, zoomAngleRange.x, zoomAngleRange.y);

        transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
