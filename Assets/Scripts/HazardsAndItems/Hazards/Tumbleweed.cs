using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tumbleweed : MonoBehaviour
{
    [SerializeField] Transform contactPoint1;
    [SerializeField] Transform contactPoint2;
    [SerializeField] float pushForce;
    [SerializeField] float speed;
    [SerializeField] float rotationFactor;
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
            transform.Rotate(0, rotationFactor, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, cp2, Time.deltaTime * speed);
            transform.Rotate(0, rotationFactor, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody player = collision.collider.GetComponent<Rigidbody>();
            Push(player);
        }

        if (collision.collider.CompareTag("Wall"))
        {
            toContactPoint1 = !toContactPoint1;
        }
    }

    void Push(Rigidbody player)
    {
        //player.AddForce(-pushForce, pushForce, 0, ForceMode.Impulse); // might have to check whether it should be x or z, also which direction tumbleweed is facing
        player.AddForce(Vector3.forward * pushForce + Vector3.up * pushForce, ForceMode.Impulse);
    }
}
