using UnityEngine;
public class ItemOnGroundState : MonoBehaviour, IItemState
{
    private ItemStateController _itemStateController;
    public void Initialize(ItemStateController itemStateController)
    {
        _itemStateController = itemStateController;
    }

    public void Enter(GameObject itemGameObject, Vector3 moveToPosition)
    {
        itemGameObject.transform.position = moveToPosition;
        itemGameObject.GetComponent<Rigidbody>().isKinematic = false;
        itemGameObject.GetComponent<Collider>().isTrigger = false;
        itemGameObject.transform.SetParent(null);
        _itemStateController.CurrentItemID = 0;
    }
}
