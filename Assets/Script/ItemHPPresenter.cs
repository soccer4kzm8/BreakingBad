using UnityEngine;
using UniRx;

public class ItemHPPresenter : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPView _hPView;
    [SerializeField] private Transform _rayOrigin;
    private IItemHPModel _itemHPModel;
    #endregion SerializeField

    #region private�ϐ�
    /// <summary>
    /// �_���[�W���󂯂�Ԋu
    /// </summary>
    private readonly float _span = 1f;

    /// <summary>
    /// �_���[�W���󂯂Ă��牽�b�o������
    /// </summary>
    private float _currentTime = 0f;
    #endregion private�ϐ�

    #region �萔
    /// <summary>
    /// �ő�HP
    /// </summary>
    private const int MAX_HP = 20;

    /// <summary>
    /// 1��Ŏ󂯂�񕜂����
    /// </summary>
    private const int RECOVERY = 1;

    /// <summary>
    /// Raycast��maxDistance
    /// </summary>
    private const float RAYCAST_MAX_DISTANCE = 500f;
    #endregion �萔

    private void Start()
    {
        _itemHPModel = new HPModel(MAX_HP, 0);
        _itemHPModel.HP.Subscribe(_ => _hPView.SetGuage(_itemHPModel.MaxHP, _itemHPModel.HP.Value)).AddTo(this);
    }

    private void Update()
    {
        // �߂��ɂ���A�C�e�����擾
        if (Physics.Raycast(_rayOrigin.position, Vector3.down, out RaycastHit hit, RAYCAST_MAX_DISTANCE) == false) return;

        if (hit.collider.CompareTag("Fire") == false) return;

        _currentTime += Time.deltaTime;

        if (_currentTime > _span)
        {
            _itemHPModel.GetRecovery(RECOVERY);
            _currentTime = 0f;
        }
    }

    /// <summary>
    /// �j������
    /// </summary>
    private void OnDestroy()
    {
        _itemHPModel.OnDestroy();
    }
}
