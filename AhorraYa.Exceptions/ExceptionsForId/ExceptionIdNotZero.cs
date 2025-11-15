using static System.Net.Mime.MediaTypeNames;

namespace AhorraYa.Exceptions.ExceptionsForId
{
    public class ExceptionIdNotZero : Exception
    {
        public string Id { get; }
        public Type Obj { get; }
        public string? text { get; set; }
        public ExceptionIdNotZero(Type obj, string id)
        {
            Id = id;
            Obj = obj;

            text = $"If you are creating a new {Obj.Name}, the Id must be 0";
        }

        public ExceptionIdNotZero()
        {
            text = "Enter a valid Id";
        }
//                    if(int.TryParse(id, out int idInt))
//            {
//                Id = idInt.ToString();
//            }
//    Obj = obj;
//            if (idInt <= 0)
//            {
//                text = "Enter a valid Id";
//            }
//            else
//{
//    text = $"If you are creating a new {Obj.Name}, the Id must be 0";
//}
        public override string Message => text!;
    }
}
