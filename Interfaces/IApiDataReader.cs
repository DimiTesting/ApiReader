namespace ApiReader.Interfaces
{
    public interface IApiDataReader
        {
            Task<string> Read(string baseAddress, string requestUri);
        }
}