using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SnakeMovement : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();

    void Update()
    {
        MouseRotationSnake();
   
    }

    private Vector3 pointInworld;
    private Vector3 mousePosition;
    private float radius = 3.0f;
    private Vector3 direction;
    void MouseRotationSnake()
    {
        Ray ray = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 1000.0f);
        mousePosition = new Vector3(hit.point.x, hit.point.y, 0);
        direction = mousePosition - transform.position;
        direction.z = 0;
        pointInworld = direction.normalized * radius + transform.position;
        transform.LookAt(pointInworld);
    }

    public float speed = 3.5f;
    public float currentRotaticn;
    public float rotationSensitvity = 50.0f;
    void FixedUpdate()
    {

        MoveForward();
        CameraFollow();
    }
    void MoveForward()
    {

        transform.position += transform.forward * speed * Time.deltaTime;
    }


    [Range(0.0f, 1.0f)]
    public float smoothTime = 0.5f;
    void CameraFollow()
    {

        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform;
        Vector3 cameraVelocity = Vector3.zero;
        camera.position = Vector3.SmoothDamp(camera.position,
        new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10), ref cameraVelocity, smoothTime);
    }


    public Transform bodyObject;
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Orb")
        {
            Destroy(other.gameObject);
            if (bodyParts.Count == 0)
            {
                Vector3 currentPos = transform.position;
                Transform newBodyPart = Instantiate(bodyObject, currentPos, Quaternion.identity) as Transform;
                bodyParts.Add(newBodyPart);
            }
            else
            {
                    Vector3 currentPos = bodyParts[bodyParts.Count-1].position;
                    Transform newBodyPart = Instantiate(bodyObject, currentPos, Quaternion.identity) as Transform;
                    bodyParts.Add(newBodyPart);
                
            }

        }
    }
}