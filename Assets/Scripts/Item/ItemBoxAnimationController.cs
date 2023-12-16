using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ItemBoxAnimationController : MonoBehaviour
{
    #region SerializeField
    /// <summary>
    /// ItemBoxアニメーター
    /// </summary>
    [SerializeField] private Animator _animator;
    #endregion SerializeField

    #region private変数
    private IInputEventProviders _playerInput;

    /// <summary>
    /// 接しているプレイヤーのCollider
    /// </summary>
    private Collider _attachPlayer = null;
    #endregion private変数

    private readonly int HashIsInput = Animator.StringToHash("IsInput");

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
    }

    private void Update()
    {
        if (_playerInput.GetCatchAndReleaseInput() == false)
        {
            return;
        }

        if (_attachPlayer == null)
        {
            return;
        }

        var itemStateController = _attachPlayer.transform.parent.GetComponent<PlayerCatchAndRelease>().PlayerItemStateController;
        if(itemStateController == null)
        {
            return;
        }

        if (itemStateController.CurrentItemID != 0)
        {
            return;
        }

        _animator.SetBool(HashIsInput, true);
    }

    public void OnCloseAnimationEnd()
    {
        _animator.SetBool(HashIsInput, false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") == true)
        {
            _attachPlayer = collider;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player") == true)
        {
            _attachPlayer = null;
        }
    }
}
