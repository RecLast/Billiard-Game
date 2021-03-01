using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamera : MonoBehaviour
{
    GameObject hayali_nesne;
    void Start()
    {
        hayali_nesne = new GameObject();
        hayali_nesne.transform.position = GameObject.Find("beyaztop").transform.position;
        transform.parent = hayali_nesne.transform;
    }

    // Update is called once per frame
    void Update()
    {
        hayali_nesne.transform.position = GameObject.Find("beyaztop").transform.position;
        float yatay_ok_tuslari = Input.GetAxis("Horizontal");
        hayali_nesne.transform.eulerAngles = new Vector3(hayali_nesne.transform.eulerAngles.x, hayali_nesne.transform.eulerAngles.y + yatay_ok_tuslari, hayali_nesne.transform.eulerAngles.z);
    }
}
