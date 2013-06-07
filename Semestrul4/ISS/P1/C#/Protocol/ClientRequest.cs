namespace Protocol
{
    public class ClientRequest
    {
        public ClientRequest()
        {
        }

        public ClientRequest(Book book, User user, string userCode, OperationType operation)
        {
            Book = book;
            User = user;
            Operation = operation;
            UserCode = userCode;
        }

        public User User { get; set; }

        public Book Book { get; set; }

        public OperationType Operation { get; set; }

        public string UserCode { get; set; }
    }
}
