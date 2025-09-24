namespace KeycloakAuth
{
    public class KeyCloakSettings
    {
        public string Realm { get; set; }
        public string Audience { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Secret { get; set; }
    }
}
