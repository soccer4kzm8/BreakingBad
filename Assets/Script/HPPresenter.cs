using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class HPPresenter : MonoBehaviour
{
    [SerializeField] private IHPModel _hPModel;
    [SerializeField] private HPView _hPView;

    private void Start()
    {
        _hPModel.HP.Subscribe(_ => _hPView.SetGuage(_hPModel.MaxHP, _hPModel.HP.Value)).AddTo(this);
    }
}
