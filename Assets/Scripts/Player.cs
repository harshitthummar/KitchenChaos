using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private gameInput gameInput;

    private float rotateSpeed = 10f;

    private bool isWalking;
    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x,0f,inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        // bool tostorebool = Physics.Raycast(vector3 origin, victor3 direction, float max distance)
        //bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize); -!!- old code

        bool canMove = !Physics.CapsuleCast(transform.position ,transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
       


        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir ,Time.deltaTime * rotateSpeed);
        
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
