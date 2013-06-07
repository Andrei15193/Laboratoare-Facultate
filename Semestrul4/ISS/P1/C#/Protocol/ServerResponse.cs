namespace Protocol
{
    public class ServerResponse
    {
        public ServerResponse()
        {
        }

        public ServerResponse(Book book, User user, OperationType operation)
        {
            Book = book;
            User = user;
            Operation = operation;
        }

        public Book Book { get; set; }

        public OperationType Operation { get; set; }

        public User User { get; set; }
    }
}
