using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IKitchenObjectParent
{

    public static Player Instance { get; private set; }
    private KitchenObject kitchenObject;


    public event EventHandler<OnSelectedCounterChangedEventsArgs> OnSelectedCounterChaned;

    public class OnSelectedCounterChangedEventsArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private gameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectholdPoint;




    private float rotateSpeed = 10f;
    private bool isWalking;

    private ClearCounter selectedCounter;

    private Vector3 lastinteractionDir;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("there is more than one player instances");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }


    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastinteractionDir = moveDir;
        }

        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastinteractionDir, out RaycastHit raycastHit, interactionDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // clearCounter.Interact();
                if (clearCounter != selectedCounter)
                {   
                    SetSelectedCounter(clearCounter);
                }

            }
            else
            {
                SetSelectedCounter(null);
            }

        }
        else
        {
            SetSelectedCounter(null);
        }
        
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        // bool tostorebool = Physics.Raycast(vector3 origin, victor3 direction, float max distance)
        //bool canMove = !Physics.Raycast(transform.position, moveDir, playerSize); -!!- old code

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);


        if (!canMove)
        {
            //cannot move towards moveDir

            //attempt only x movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
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
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChaned?.Invoke(this, new OnSelectedCounterChangedEventsArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectholdPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
