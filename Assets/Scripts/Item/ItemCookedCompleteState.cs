using System;
using UnityEngine;
public class ItemCookedCompleteState : IItemState
{
    #region private変数
    /// <summary>
    /// Itemステートコントローラ
    /// </summary>
    private readonly ItemStateController _itemStateController;
    #endregion private変数

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="displayThing">表示させるもの</param>
    public ItemCookedCompleteState(ItemStateController itemStateController)
    {
        try
        {
            if (itemStateController == null)
            {
                throw new NullReferenceException();
            }
        }
        catch (NullReferenceException ex)
        {
            Debug.LogError(ex);
        }
        _itemStateController = itemStateController;
    }

    /// <summary>
    /// CookedCompleteステートに入った時の処理
    /// </summary>
    public void Enter()
    {
        foreach (var displayThing in _itemStateController.DisplayThings)
        {
            displayThing.SetActive(true);
        }

        _itemStateController.HP.SetActive(false);
    }

    public void Exit()
    {

    }
}
