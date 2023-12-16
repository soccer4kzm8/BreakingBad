using UnityEngine;

public class ItemInItemState : IItemState
{
    #region private変数
    /// <summary>
    /// 表示させるもの
    /// </summary>
    private GameObject _displayThing = null;
    #endregion private変数

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="displayThing">表示させるもの</param>
    public ItemInItemState(GameObject displayThing)
    {
        _displayThing = displayThing;
    }

    /// <summary>
    /// Defaultステートに入った時の処理
    /// </summary>
    public void Enter()
    {
        if (_displayThing == null) return;

        _displayThing.SetActive(true);
    }
}
