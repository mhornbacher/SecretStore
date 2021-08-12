namespace SecretStore.Core
{
    public struct Secret
    {
        public string Key { get; internal set; }
        public string Value { get; internal set; }
    }
}
