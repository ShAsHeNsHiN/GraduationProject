public interface IPlayerOpenOrCloseHand
{
    /// <summary>
    /// 沒有抓任何東西
    /// </summary>
    public void OpenHand();

    /// <summary>
    /// 抓著某樣東西
    /// </summary>
    public void CloseHand();
}
