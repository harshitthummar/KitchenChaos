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


        if (!canMove)
        {
            //cannot move towards moveDir

            //attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //can move only on x
                moveDir = moveDirX;
            }
            else
            {
                //cannot move to x

                //atttept to move z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //can move only on Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //can not move in any directio
                    //movement not possible
                }

            }
        }

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
