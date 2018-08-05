using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FICameraController{

    public GameObject Placeholder = null;
    public Camera Cam;

    private Vector3 m_placeholderPrevRotation;

    public FICameraController(Camera cam, GameObject placeholder)
    {
        Cam = cam;
        Placeholder = placeholder;
    }

    public void Init()
    {
        if (Placeholder != null)
        {
            Cam.transform.position = Placeholder.transform.position;
            Cam.transform.rotation = Placeholder.transform.rotation;
        }
    }

    public void Update()
    {
        if(Placeholder != null)
        {
            Cam.transform.position = Placeholder.transform.position;
            if (m_placeholderPrevRotation != Placeholder.transform.rotation.eulerAngles)
            {
                Vector3 rotationBasedOfPlaceholder = Cam.transform.rotation.eulerAngles - m_placeholderPrevRotation;
                Cam.transform.eulerAngles = Placeholder.transform.rotation.eulerAngles + rotationBasedOfPlaceholder;
            }
            m_placeholderPrevRotation = Placeholder.transform.rotation.eulerAngles;
        }

    }

}
