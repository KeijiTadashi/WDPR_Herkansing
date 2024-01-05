public interface IUser
{
    private string username { get; set; }
    private string password { get; set; }
    private int identifier { get; set; }
    private string role { get; set; }
    private void Chat(User u, string message)
    {

    }
}