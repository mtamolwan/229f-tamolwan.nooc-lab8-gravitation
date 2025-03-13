using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic; //ถ้า List<Gravity> ขึ้นแดง

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.00667f;

    //List of Objects
    public static List<Gravity> gravityObjectsList;

    [SerializeField] bool planet = false; // if not a planet > orbit
    [SerializeField] int orbitSpeed = 1000;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (gravityObjectsList == null)
        {
            gravityObjectsList = new List<Gravity>();

        }
        gravityObjectsList.Add(this); //เพิ่มobject ให้กับ List

        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }

    private void FixedUpdate() //เรียกไวกว่า Update
    {
        foreach (var obj in gravityObjectsList)
        {
            //Call Attract
            if (obj != this)
                Attract(obj);

        }

    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude; //เอาค่าระยะทางจาก Vector3 ไม่มีทิศเกี่ยว

        float forceMagnitude = G * (rb.mass * otherRb.mass / Mathf.Pow(distance, 2)); //สูตรแรงโน้มถ่วง
        Vector3 gravityForce = forceMagnitude * direction.normalized; // normalized = เอาทิศจาก Vector3

        otherRb.AddForce(gravityForce);
    }
}