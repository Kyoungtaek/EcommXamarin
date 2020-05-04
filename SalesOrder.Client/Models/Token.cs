namespace SalesOrder.Client.Models
{
    public class Token
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public int Expires_In { get; set; }
        public int Creation_Time { get; set; }
        public int Expiration_Time { get; set; }
    }
}
