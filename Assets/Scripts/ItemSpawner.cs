using UniRx;
using UniRx.Triggers;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    /// <summary>
    /// プレイヤー当たり判定Collider
    /// </summary>
    [SerializeField] private GameObject _collider;

    private IInputEventProviders _playerInput;

    /// <summary>
    /// 拾う・放すの入力がされたかどうか
    /// </summary>
    private bool _getCatchAndReleaseInput = false;

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();

        _collider.OnTriggerStayAsObservable()
            .Where(collider => collider.CompareTag("Player"))
            .Where(_ => _getCatchAndReleaseInput == true)
            .Where(collider => CheckOpenItemBox(collider))
            .Subscribe(collider =>
            {
                var prefab = Instantiate(_prefab);
                var prefabCollider = prefab.GetComponent<Collider>();
                var playerCatchAndRelease = collider.transform.parent.GetComponent<PlayerCatchAndRelease>();
                playerCatchAndRelease.CatchItem(prefabCollider);
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

    /// <summary>
    /// アイテムボックスを開くかのチェック
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    private bool CheckOpenItemBox(Collider collider)
    {
        _getCatchAndReleaseInput = false;

        if (collider.transform.parent.GetComponent<PlayerCatchAndRelease>().CurrentItem == null)
        {
            return true;
        }

        return false;
    }
}
