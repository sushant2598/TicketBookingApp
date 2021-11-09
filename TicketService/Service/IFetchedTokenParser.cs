namespace TicketService.Service
{
    public interface IFetchedTokenParser
    {
        string[] tokenValues(string fetchedToken);
    }
}
