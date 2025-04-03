public class CharaterTop : Charater
{
    public static CharaterTop Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }
}
