using UnityEngine;
using UnityEngine.UI;

public class HPView : MonoBehaviour
{
    /// <summary>
    /// スライダー本体
    /// </summary>
    [SerializeField] private Slider _hpGuage;

    /// <summary>
    /// ゲージ背景
    /// </summary>
    [SerializeField] private Image _background;

    /// <summary>
    /// ゲージ埋まる部分のイメージ
    /// </summary>
    [SerializeField] private Image _fill;

    /// <summary>
    /// HPゲージの設定
    /// </summary>
    /// <param name="maxHP">最大HP</param>
    /// <param name="hp">残っているHP</param>
    public void SetGuage(int maxHP, float hp)
    {
        _hpGuage.value = hp / maxHP;
    }

    /// <summary>
    /// ゲージの表示・非表示
    /// </summary>
    /// <param name="active"></param>
    public void SetInvisible(bool active)
    {
        Debug.LogError("入った");
        _background.enabled = active;
        _fill.enabled = active;
    }
}
