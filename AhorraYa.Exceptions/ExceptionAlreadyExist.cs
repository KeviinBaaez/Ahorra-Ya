namespace AhorraYa.Exceptions
{
    public class ExceptionAlreadyExist : Exception
    {
        public Type Obj { get; }
        public ExceptionAlreadyExist(Type obj)
        {
            Obj = obj;
        }

        public override string Message => $"An {Obj.Name} with the same name already exists.";
    }
}
