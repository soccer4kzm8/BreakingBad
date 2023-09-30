using UnityEngine;

public class ItemConstraintsManager : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// 地面に着いているかどうかを設定
    /// </summary>
    /// <param name="isHangingInAir"></param>
    public void SetIsItemOnGround(bool isOnGround)
    {
        if(_rigidbody == null)
        {
            _rigidbody = this.GetComponent<Rigidbody>();
        }
        if(isOnGround == true)
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Stage"))
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
