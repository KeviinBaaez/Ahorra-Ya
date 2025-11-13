namespace AhorraYa.Exceptions.ExceptionsForId
{
    public class ExceptionIdNotFound : Exception
    {
        public string Id { get; }
        public Type Obj { get; }
        public ExceptionIdNotFound(Type obj, string id)
        {
            Obj = obj;
            Id = id;
        }
        public override string Message => $"No {Obj.Name} was found with Id: {Id} ";
    }
}