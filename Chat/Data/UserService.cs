namespace Chat.Data
{
    public class UserService
    {
        private readonly Dictionary<string, string> _users = new();

        public void Add(string connectionId, string username) 
        {
            _users.Add(connectionId, username);
        }

        public void RemoveByName(string username)
        {
            foreach (var (id, name) in _users)
            {
                if (name == username)
                {
                    _users.Remove(id);
                    return;
                }
            }
        }

        public string GetConnectionIdByName(string username)
        {
            foreach (var (id, name) in _users)
            {
                if (name == username)
                {
                    return id;
                }
            }

            return "";
        }

        public IEnumerable<(string ConnectionId, string Username)> GetAll()
        {
            return _users.Select(pair => (pair.Key, pair.Value));
        }
    }
}
