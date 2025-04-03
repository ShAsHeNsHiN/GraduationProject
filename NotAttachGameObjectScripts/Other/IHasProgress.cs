using System;

public interface IHasProgress
{
    public event EventHandler<OnPrgressChangedEventArgs> OnProgressChanged;

    public class OnPrgressChangedEventArgs:EventArgs{
        public float progressNormalized;
    }
}
