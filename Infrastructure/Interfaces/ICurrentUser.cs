namespace Infrastructure.Interfaces
{
    public interface ICurrentUser
    {
        string GetUsername();

        bool IsAuthenticated();
    }
}