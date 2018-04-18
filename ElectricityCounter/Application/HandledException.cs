namespace ElectricityCounter.Application
{
    ///<Summary>
    /// Exception Type for handeled cases.
    ///</Summary>
    public class HandledException : System.Exception
    {
        public ThrowResponseLevel throwResponseLevel { get; set; }

        public enum ThrowResponseLevel
        {
            Unathorized = -1,
            BadRequest = 0,
            Ok = 1
        }

        public HandledException()
        {
        }

        public HandledException(string message, ThrowResponseLevel responseLevel) : base(message)
        {
            throwResponseLevel = responseLevel;
        }

        public HandledException(string message) : base(message)
        {
        }

        public HandledException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}