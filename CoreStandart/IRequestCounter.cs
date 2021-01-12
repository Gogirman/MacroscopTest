namespace CoreStandart
{
    public interface IRequestCounter
    {
        int GetCounter();
        void Increase();
        void Decrease();
    }

    public class RequestCounter : IRequestCounter
    {
        private int _counter;
        private object obj = new object();

        public int GetCounter()
        {
            lock (obj)
            {
                return _counter;
            }
        }

        public void Increase() 
        {
            lock (obj)
            {
                _counter++;
            }
        }
        public void Decrease()
        {
            lock (obj)
            {
                _counter--;
            }
        }
    }
}