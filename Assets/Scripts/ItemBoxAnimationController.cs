using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class ItemBoxAnimationController : MonoBehaviour
{
    #region SerializeField
    /// <summary>
    /// プレイヤー当たり判定Collider
    /// </summary>
    [SerializeField] private GameObject _collider;

    /// <summary>
    /// ItemBoxアニメーター
    /// </summary>
    [SerializeField] private Animator _animator;
    #endregion SerializeField

    #region private変数
    private IInputEventProviders _playerInput;

    /// <summary>
    /// 拾う・放すの入力がされたかどうか
    /// </summary>
    private bool _getCatchAndReleaseInput = false;
    #endregion private変数

    private readonly int HashIsInput = Animator.StringToHash("IsInput");

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();

        _collider.OnTriggerStayAsObservable()
            .Where(collider => collider.CompareTag("Player"))
            .Where(_ => _getCatchAndReleaseInput == true)
            .Subscribe(collider =>
            {
                _getCatchAndReleaseInput = false;
                _animator.SetBool(HashIsInput, true);
                Debug.Log(collider.gameObject.name);
            }).AddTo(this);
    }

    private void Update()
    {
        if (_playerInput.GetCatchAndReleaseInput())
        {
            _getCatchAndReleaseInput = true;
        }
        else
        {
            _getCatchAndReleaseInput = false;
        }
    }

    public void OnCloseAnimationEnd()
    {
        Debug.Log("閉じる");

        _animator.SetBool(HashIsInput, false);
    }
}
