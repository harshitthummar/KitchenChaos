using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    

    private KitchenObject kitchenObject;

    
    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform KitchenObjectTransform = Instantiate(KitchenObjectSO.prefab, counterTopPoint);
            KitchenObjectTransform.GetComponent<KitchenObject>().SetIKitchenObjectParent(this);
           
        }
        else
        {
            //give the object to the player
            kitchenObject.SetIKitchenObjectParent(player);

           // Debug.Log(kitchenObject.GetClearCounter());
        }
        
        
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
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
