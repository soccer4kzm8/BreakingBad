using UnityEngine;
using UniRx;

public class HPPresenter : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPView _hPView;
    private IHPModel _hPModel;
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
    private const int MAX_HP = 100;
    #endregion �萔

    private void Start()
    {
        _hPModel = new HPModel(MAX_HP);
        _hPModel.HP.Subscribe(_ => _hPView.SetGuage(_hPModel.MaxHP, _hPModel.HP.Value)).AddTo(this);
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _span)
        {
            _hPModel.GetDamage(1);
            _currentTime = 0f;
        }
    }

    /// <summary>
    /// �j������
    /// </summary>
    private void OnDestroy()
    {
        _hPModel.OnDestroy();
    }
}
