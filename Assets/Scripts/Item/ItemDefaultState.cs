using UnityEngine;

public class ItemDefaultState : IItemState
{
    private GameObject _displayThing = null;
    public ItemDefaultState(GameObject displayThing)
    {
        _displayThing = displayThing;
    }

    /// <summary>
    /// Defaultステートに入った時の処理
    /// </summary>
    public void Enter()
    {
        if (_displayThing == null) return;

        _displayThing.SetActive(false);
    }
}
