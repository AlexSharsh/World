using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] float _Speed = 5.0f;
    private Quaternion originRotation;
    float angleX;
    float angleY;

    private void Awake()
    {
        originRotation = transform.rotation;
        angleX = originRotation.eulerAngles.x;
        angleY = originRotation.eulerAngles.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (angleX < 0)
            angleX = 360;
        if (angleX > 360)
            angleX = 0;

        if (angleY < 0)
            angleY = 360;
        if (angleY > 360)
            angleY = 0;

        angleX += Time.fixedDeltaTime * _Speed;

        Quaternion rotateX = Quaternion.AngleAxis(angleX, new Vector3(1, 0, 0));
        Quaternion rotateY = Quaternion.AngleAxis(angleY, Vector3.right);

        transform.rotation = originRotation * rotateX * rotateY;
    }
}
