namespace Mayfly.Wild
{
    public interface IBio
    {
        bool IsAvailable(string name);


        double GetValue(string name, object x);


        void Refresh();

        bool Refresh(string name);

        void Recombine();
    }
}
