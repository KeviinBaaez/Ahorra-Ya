namespace AhorraYa.Exceptions
{
    public class ExceptionByServiceConnection : Exception
    {
        public override string Message => "An error occurred while connecting to SQL services.";
    }
}
