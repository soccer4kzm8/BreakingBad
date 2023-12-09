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
    /// 接しているプレイヤーのCollider
    /// </summary>
    private Collider _attachPlayer = null;

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

        if(_attachPlayer == null)
        {
            return;
        }

        var itemStateController = _attachPlayer.transform.parent.GetComponent<PlayerCatchAndRelease>().ItemStateController;
        if (itemStateController == null)
        {
            return;
        }

        if (itemStateController.CurrentItemID != 0)
        {
            return;
        }

        var prefab = Instantiate(_prefab);
        var prefabCollider = prefab.GetComponent<Collider>();
        var playerCatchAndRelease = _attachPlayer.transform.parent.GetComponent<PlayerCatchAndRelease>();
        playerCatchAndRelease.CatchItem(prefabCollider);
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
