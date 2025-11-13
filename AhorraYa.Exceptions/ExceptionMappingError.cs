namespace AhorraYa.Exceptions
{
    public class ExceptionMappingError: Exception
    {
        public override string Message => "An error occurred while mapping the domain object.";
    }

    public class ExceptionRequestMappingError : Exception
    {
        public override string Message => "The values ​​entered are not valid";
    }
}
