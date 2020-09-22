using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerController>().gameObject.transform.position = transform.position;
        GameObject deleteCamera = GameObject.FindGameObjectWithTag("DeleteCamera");
        if (deleteCamera)
        {
            deleteCamera.SetActive(false);
        }
    }
}
