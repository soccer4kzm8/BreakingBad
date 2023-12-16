using UnityEngine;

public class PlayerItemHoldState : MonoBehaviour, IPlayerItemState
{
    private Transform _moveToTrasform;

    public void Initialize(Transform transform)
    {
        _moveToTrasform = transform;
    }

    public void Enter(GameObject itemGameObject, Vector3 moveToPosition)
    {
        itemGameObject.transform.SetParent(_moveToTrasform);
        itemGameObject.transform.localPosition = moveToPosition;
        itemGameObject.GetComponent<Rigidbody>().isKinematic = true;
        itemGameObject.GetComponent<Collider>().isTrigger = true;
    }
}
