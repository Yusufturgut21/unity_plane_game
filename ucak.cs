using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ucak : MonoBehaviour
{
    public float speed;
    public float maxspeed;
    public float minspeed;
    public float rotspeed1;
    public float rotspeed2;

    public GameObject[] kameralar;
    public int kamerasayi;

    private Quaternion initialRotation;
    private GameObject heldObject;
    private bool isHoldingObject = false;

    void Start()
    {
        for (int i = 0; i < kameralar.Length; i++)
        {
            kameralar[i].SetActive(false);
        }
        kameralar[kamerasayi].SetActive(true);

        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            kameralar[kamerasayi].SetActive(false);
            kamerasayi++;
            if (kamerasayi > kameralar.Length - 1)
            {
                kamerasayi = 0;
            }
            kameralar[kamerasayi].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isHoldingObject)
            {
                ReleaseObject();
            }
            else
            {
                TryPickupObject();
            }
        }

        transform.position += transform.forward * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.W))
        {
            if (speed < maxspeed) speed++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (speed > minspeed) speed--;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotspeed1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * Time.deltaTime * rotspeed1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.left * Time.deltaTime * rotspeed1);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.right * Time.deltaTime * rotspeed1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * rotspeed2);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.down * Time.deltaTime * rotspeed2);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCarRotation();
        }
    }

    private void ResetCarRotation()
    {
        transform.rotation = initialRotation;
    }

    private void TryPickupObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f); // Dronun etrafýndaki cisimleri kontrol etmek için bir alan oluþturuyoruz.
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("PickupObject")) // Etiketi "PickupObject" olan cisimleri kontrol ediyoruz.
            {
                heldObject = collider.gameObject;
                heldObject.transform.parent = transform;
                heldObject.transform.localPosition = Vector3.zero;
                isHoldingObject = true;
                break; // Ýlk bulduðumuz cisimi tuttuktan sonra döngüden çýkýyoruz.
            }
        }
    }

    private void ReleaseObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.parent = null;
            heldObject = null;
            isHoldingObject = false;
        }
    }
}
