using System;
using UnityEngine;
public class ItemCookingState : IItemState
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
    public ItemCookingState(ItemStateController itemStateController)
    {
        try
        {
            if(itemStateController == null)
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
    /// Cookedステートに入った時の処理
    /// </summary>
    public void Enter()
    {
        foreach (var displayThing in _itemStateController.DisplayThings)
        {
            displayThing.SetActive(true);
        }

        _itemStateController.HP.SetActive(true);
        _itemStateController.Animator.SetBool("IsMix", true);
    }

    /// <summary>
    /// Cookedステートから出るとき処理
    /// </summary>
    public void Exit()
    {
        _itemStateController.Animator.SetBool("IsMix", false);
    }
}
