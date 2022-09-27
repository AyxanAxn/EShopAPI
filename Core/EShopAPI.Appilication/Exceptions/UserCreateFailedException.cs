namespace EShopAPI.Appilication.Exceptions
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException() : base("When the user created there was thrown an exception.")
        {

        }

        public UserCreateFailedException(string? message) : base(message)
        {
        }

        public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}