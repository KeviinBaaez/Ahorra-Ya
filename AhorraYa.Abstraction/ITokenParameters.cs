namespace AhorraYa.Abstractions
{
    public interface ITokenParameters
    {
        string UserName { get; set; }
        string Email { get; set; }
        string PasswordHash { get; set; }
        string Id { get; set; }
    }
}
