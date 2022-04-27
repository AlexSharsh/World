using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] Rbs;
    private Collider[] Colls;

    public float killForce = 5f;

    private Animator anim;
    private ThirdPersonUserControl controller;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Rbs = GetComponentsInChildren<Rigidbody>();
        Colls = GetComponentsInChildren<Collider>();

        controller = GetComponent<ThirdPersonUserControl>();
        characterController = GetComponent<CharacterController>();

        Revive();
    }

    private void Kill()
    {
        SetRagdoll(true);
        SetMain(false);
    }

    private void Revive()
    {
        SetRagdoll(false);
        SetMain(true);
    }

    void SetRagdoll(bool active)
    {
        for (int i = 0; i < Rbs.Length; i++)
        {
            Rbs[i].isKinematic = !active;
            if (active)
            {
                Rbs[i].AddForce(Vector3.forward * killForce, ForceMode.Impulse);
            }
        }

        for (int i = 0; i < Colls.Length; i++)
        {
            Colls[i].enabled = active;
        }
    }

    void SetMain(bool active)
    {
        anim.enabled = active;
        characterController.enabled = active;
        controller.enabled = active;
        Rbs[0].isKinematic = !active;
        Colls[0].enabled = active;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Kill();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Revive();
        }
    }
}
