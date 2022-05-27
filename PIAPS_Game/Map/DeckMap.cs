namespace PIAPS_Game.Map;

public class DeckMap : AbstractMap
{
    protected int _size;

    public DeckMap() : this(5) { }

    public DeckMap(int size)
    {
        Size = size;
    }

   

    public int Size
    {
        get => _size;
        protected set => _size = value;
    }

    public override void Update<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs)
    {
        throw new NotImplementedException();
    }
}