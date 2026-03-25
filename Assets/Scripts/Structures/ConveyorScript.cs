using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    private void Start()
    {
        foreach (MeshRenderer meshRenderer in base.GetComponentsInChildren<MeshRenderer>()) //Get All Meshs
        {
            meshs.Add(meshRenderer);
        }
        moveSpeed = beltspeed / 4;
    }
    private void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (offset == 0 || offset == 3) //forward belt
            {
                vector.y += beltspeed * Time.deltaTime;
                if (vector.y >= 20f)
                {
                    vector.y = 0f;
                }
                foreach (MeshRenderer meshRenderer in meshs)
                {
                    meshRenderer.material.SetTextureOffset("_MainTex", vector);
                }
            }
            else //Back belt
            {
                vector.y -= beltspeed * Time.deltaTime;
                if (vector.y <= 0f)
                {
                    vector.y = 20f;
                }
                foreach (MeshRenderer meshRenderer in meshs)
                {
                    meshRenderer.material.SetTextureOffset("_MainTex", vector);
                }
            }
        }
		UIPopupTextManagerWithMovement.Show("Struct_Conveyor_Motor", Color.white, transform, 99f, 0f);
    }
    private void OnTriggerStay(Collider other)
    {
        Vector3 addedVelocity = Vector3.zero;
        if (offset == 0) //forward
        {
            addedVelocity = new Vector3(1, 0, 0);
        }
        else if (offset == 1) //back
        {
            addedVelocity = new Vector3(-1, 0, 0);
        }
        else if (offset == 2) //left
        {
            addedVelocity = new Vector3(0, 0, 1);
        }
        else if (offset == 3) //right
        {
            addedVelocity = new Vector3(0, 0, -1);
        }
        if (other.tag == "Player")
        {
            other.GetComponent<CharacterController>().Move(addedVelocity * MoveOffset * moveSpeed);
        }
        if (other.tag == "NPC")
        {
            other.transform.position += (addedVelocity * MoveOffset * moveSpeed);
        }
    }
    public float beltspeed;
    private float moveSpeed;
    public List<MeshRenderer> meshs;
    private Vector2 vector;
    public int offset, MoveOffset;
}
