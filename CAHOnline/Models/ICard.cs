namespace CAHOnline.Models
{
    public interface ICard
    {
        string Text { get; }
    }

    public interface IBlackCard : ICard { }
}
