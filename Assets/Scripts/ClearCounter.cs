using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    public void Interact()
    {
        Debug.Log("interact");
        Transform KitchenObjectSOTransform = Instantiate(KitchenObjectSO.prefab, counterTopPoint);
        KitchenObjectSOTransform.localPosition = Vector3.zero;

        Debug.Log(KitchenObjectSOTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }
}
