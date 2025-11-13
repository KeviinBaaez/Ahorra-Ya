namespace AhorraYa.Exceptions.ExceptionsForId
{
    public class ExceptionIdNotZero : Exception
    {
        public string Id { get; }
        public Type Obj { get; }
        public ExceptionIdNotZero(Type obj, string id)
        {
            Id = id;
            Obj = obj;
        }

        public override string Message => $"If you are creating a new {Obj.Name}, the Id must be 0";
    }
}
