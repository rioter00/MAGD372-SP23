using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FloatingPlatform : MonoBehaviour
{
    [SerializeField] Transform contactPoint1;
    [SerializeField] Transform contactPoint2;
    [SerializeField] float speed;
    Vector3 cp1;
    Vector3 cp2;
    bool toContactPoint1 = true;
    float initialY;

    private void Start()
    {
        initialY = transform.position.y;
    }

    private void Update()
    {
        cp1 = new Vector3(contactPoint1.position.x, initialY, contactPoint1.position.z);
        cp2 = new Vector3(contactPoint2.position.x, initialY, contactPoint2.position.z);

        if (toContactPoint1)
        {
            transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, cp1, Time.deltaTime * speed);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, cp2, Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            toContactPoint1 = !toContactPoint1;
        }

        if (collision.collider.CompareTag("Player"))
        {
            Transform player = collision.collider.GetComponent<Transform>();
            player.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Transform player = collision.collider.GetComponent<Transform>();
            player.SetParent(null);
        }
    }
}
