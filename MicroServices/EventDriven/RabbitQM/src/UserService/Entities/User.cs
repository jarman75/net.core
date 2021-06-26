namespace UserService.Entities
{
    public class User
    {
        public int ID {get; set;}   
        public string Name {get; set;}
        public string Mail {get; set;}
        public string OtherData {get; set;}
        private int _version = 0;
        public int Version { get => _version; internal set => _version++; }
    }
}