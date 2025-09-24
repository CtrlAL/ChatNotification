namespace KeycloakAuth
{
    internal class KeyCloakSettings
    {
        public string Realm { get; set; }
        public string Audience { get; set; }
        public int Port { get; set; }
        public int Host { get; set; }
        public string Secret { get; set; }
    }
}
