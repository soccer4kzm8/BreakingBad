using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region SerializeField
    /// <summary>
    /// プレイヤー当たり判定Collider
    /// </summary>
    [SerializeField] private GameObject _collider;

    [SerializeField] private PoolManager poolManager;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPos;
    #endregion SerializeField

    #region private変数
    /// <summary>
    /// 拾う・放すの入力がされたかどうか
    /// </summary>
    private bool _getCatchAndReleaseInput = false;

    private IInputEventProviders _playerInput;
    #endregion private変数

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();

        _collider.OnTriggerStayAsObservable()
            .Where(collider => collider.CompareTag("Player"))
            .Where(_ => _getCatchAndReleaseInput == true)
            .Where(collider => CheckOpenItemBox(collider))
            .Subscribe(collider =>
            {
                Spawn(prefab);
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

    /// <summary>
    /// 生成
    /// </summary>
    /// <param name="prefab"></param>
    private void Spawn(GameObject prefab)
    {
        Destroyer destroyer = poolManager.GetGameObject(prefab, spawnPos.position, Quaternion.identity).GetComponent<Destroyer>();
        destroyer.PoolManager = poolManager;
    }
}
