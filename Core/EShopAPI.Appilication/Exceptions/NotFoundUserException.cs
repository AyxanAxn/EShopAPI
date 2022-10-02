namespace EShopAPI.Appilication.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("Username or password is wrong!")
        { }
        public NotFoundUserException(string message) : base(message)
        { }
    }
}
