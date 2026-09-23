namespace _01_Scripts.Interactables.Item
{
    public interface IItem
    {
        public bool IsDisposable { get; set; }
        public bool IsRightNow { get; set; }
        public Player.Player Player { get; set; }
        public void UseItem();
        public void SecondaryUseItem();
    }
}