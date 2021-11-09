namespace TicketService.Service
{
    public class FetchedTokenParser : IFetchedTokenParser
    {
        public string[] tokenValues(string fetchedToken)
        {
            return fetchedToken.Split(" ");
        }
    }
}
