using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float baseSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float sprintSpeed = 5f;

    private bool moving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private bool rotating = false;
    private Quaternion startRotation;
    private Quaternion endRotation;

    public float movementTime;
    public float rotationTime;
    private float transitionTimeCounter;

    [SerializeField] private LayerMask environment;
    [SerializeField] private LayerMask floor;

    void Update() {
        if (!moving && !rotating) {
            Rotate();
            Move();
        } else {
            Translate();
        }
    }

    void Rotate() {
        float x = Input.GetAxisRaw("Horizontal");
        if (x > 0) {
            endRotation = transform.rotation * Quaternion.Euler(0, 90, 0);
            rotating = true;
        } else if (x < 0) {
            endRotation = transform.rotation * Quaternion.Euler(0, -90, 0);
            rotating = true;
        }
    }

    void Move() {
        float z = Input.GetAxis("Vertical");
        RaycastHit hit;
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), transform.forward * 4, Color.yellow);
        if (z > 0) {
            if (!(Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), transform.forward, out hit, 4, 1 << environment))) {
                endPosition = transform.position + transform.forward * 4f;
                Physics.Raycast(new Vector3(endPosition.x, transform.position.y + 1, endPosition.z), Vector3.down, out hit, 4, floor);
                endPosition = new Vector3(endPosition.x, (Mathf.Round(hit.point.y / 0.5f) * 0.5f) + 1, endPosition.z);
                moving = true;
            }
        } else if (z < 0) {
            if (!(Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), transform.forward * -1, out hit, 4, 1 << environment))) {
                endPosition = transform.position - transform.forward * 4f;
                Physics.Raycast(new Vector3(endPosition.x, endPosition.y + 1, endPosition.z), Vector3.down, out hit, 4, floor);
                endPosition = new Vector3(endPosition.x, (Mathf.Round(hit.point.y / 0.5f) * 0.5f) + 1, endPosition.z);
                moving = true;
            }
        } 
    }

    void Translate() {
        if (moving) {
            if (transform.position != endPosition && endPosition != Vector3.zero) {
                transitionTimeCounter += Time.deltaTime/movementTime;
                transform.position = Vector3.Lerp(transform.position, endPosition, transitionTimeCounter);
            } else {
                transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y / 0.5f) * 0.5f, Mathf.Round(transform.position.z));
                moving = false;
                transitionTimeCounter = 0;
                endPosition = Vector3.zero;
            }
        } else if (rotating) {
            if (transform.rotation != endRotation) {
                //Debug.Log("Rotating...");
                transitionTimeCounter += Time.deltaTime/rotationTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, transitionTimeCounter);
            } else {
                transform.rotation = Quaternion.Euler(0, Mathf.Round(transform.eulerAngles.y / 90) * 90, 0);
                rotating = false;
                transitionTimeCounter = 0;
                endRotation = transform.rotation;
            }
        }
    }
}