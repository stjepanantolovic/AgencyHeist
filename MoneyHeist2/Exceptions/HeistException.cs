namespace MoneyHeist2.Exceptions
{
    public class HeistException : Exception
    {
        public string UserMessage { get; set; }
        public HeistException()
        {

        }
        public HeistException(string message) : base(message)
        {
            UserMessage = message;
        }

        public HeistException(string userMessage, string message) : base(message)
        {
            UserMessage = userMessage;
        }


        public HeistException(string userMessage, string message, Exception inner) : base(message, inner)
        {
            UserMessage = userMessage;
        }

    }
}
