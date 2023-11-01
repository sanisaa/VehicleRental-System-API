namespace API.Models
{
    public class ModelBase
    {
        public int Id { get; set; }
    }
}


//will inherit all the models through this
//contains only Id to prevent writing Id again and again