using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    enum SpotlightMode
    {
        Fixed,
        Rotate
    };

    [SerializeField] SpotlightMode _Mode = SpotlightMode.Fixed;
    [SerializeField] float _Speed = 30.0f;
    [SerializeField] Light _Spot;
    [SerializeField] Sun _Sun;
    private Quaternion originRotation;
    float angleY;

    private void Awake()
    {
        originRotation = transform.rotation;
        angleY = originRotation.eulerAngles.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if ((-180 < _Sun.transform.rotation.ToEuler().x) && (_Sun.transform.rotation.ToEuler().x < 0) )
        {
            _Spot.enabled = true;
        }
        else
        {
            _Spot.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_Mode == SpotlightMode.Rotate)
        {
            if (_Spot.enabled)
            {
                if (angleY < 0)
                    angleY = 360;
                if (angleY > 360)
                    angleY = 0;

                angleY += Time.fixedDeltaTime * _Speed;

                Quaternion rotateY = Quaternion.AngleAxis(angleY, new Vector3(0, 0, 1));

                transform.rotation = originRotation * rotateY;
            }
        }
    }
}
