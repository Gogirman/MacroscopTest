namespace CoreStandart
{
    public interface ISingletone
    {
        int GetCounter();
        void Increase();
        void Decrease();
    }

    public class Singletone : ISingletone
    {
        private int _counter;

        public int GetCounter()
        {
            return _counter;
        }

        public void Increase() => _counter++;
        public void Decrease() => _counter--;
    }
}