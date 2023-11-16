using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent iKitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetIKitchenObjectParent(IKitchenObjectParent iKitchenObjectParent)
    {
        if(this.iKitchenObjectParent != null)
        {
            this.iKitchenObjectParent.ClearKitchenObject();
        }

        this.iKitchenObjectParent = iKitchenObjectParent;
        if (iKitchenObjectParent.HasKitchenObject() )
        {
            Debug.LogError("Kitchenobjectparent already has a kitchenobject");
        }

        iKitchenObjectParent.SetKitchenObject(this);

        transform.parent = iKitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetIKitchenObjectParent()
    {
        return iKitchenObjectParent;
    }
}
