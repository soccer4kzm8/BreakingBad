using UnityEngine;

public class PestleAnimationController : MonoBehaviour
{
    /// <summary>
    /// PestleBoxアニメーター
    /// </summary>
    [SerializeField] private Animator _animator;

    private IInputEventProviders _playerInput;

    /// <summary>
    /// 接しているプレイヤーのCollider
    /// </summary>
    private Collider _attachPlayer = null;

    private readonly int HashIsMix = Animator.StringToHash("IsMix");


    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
    }


    private void Update()
    {
        if (_playerInput.GetThrowAndMixInput() == false)
        {
            return;
        }

        if (_attachPlayer == null)
        {
            return;
        }

        var currentItem = _attachPlayer.transform.parent.GetComponent<PlayerCatchAndRelease>().CurrentItem;
        if (currentItem != null)
        {
            return;
        }

        _animator.SetBool(HashIsMix, true);
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
            _animator.SetBool(HashIsMix, false);
        }
    }
}
