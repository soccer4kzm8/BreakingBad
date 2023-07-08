using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ItemHPPresenter : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPView _hPView;
    [SerializeField] private GameObject _item;
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
    #endregion �萔

    private void Start()
    {
        _itemHPModel = new HPModel(MAX_HP, 0);
        // HP���ω�������Q�[�W�ɔ��f
        _itemHPModel.HP.Subscribe(_ => _hPView.SetGuage(_itemHPModel.MaxHP, _itemHPModel.HP.Value)).AddTo(this);
        // Fire�ɓ������Ă���Ԃ́A�A�C�e���̗͂̉񕜂��s��
        _item.OnCollisionStayAsObservable()
            .Where(collision => collision.collider.CompareTag("Fire"))
            .Subscribe(_ =>
            {
                _currentTime += Time.deltaTime;

                if (_currentTime > _span)
                {
                    _itemHPModel.GetRecovery(RECOVERY);
                    _currentTime = 0f;
                }
            }).AddTo(this);
        // Fire�ɐG�ꂽ��Q�[�W�̕\��
        _item.OnCollisionEnterAsObservable()
            .Where(collision => collision.collider.CompareTag("Fire"))
            .Subscribe(_ => _hPView.SetInvisible(true)).AddTo(this);
    }

    /// <summary>
    /// �j������
    /// </summary>
    private void OnDestroy()
    {
        _itemHPModel.OnDestroy();
    }
}
