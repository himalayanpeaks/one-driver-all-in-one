using OneDriver.Framework.Libs.Validator;

namespace OneDriver.Framework.Module
{
    public enum ConnectionError
    {
        NoError,
        AccessDenied,
        CommunicaionError,
        ErrorInDisconnecting
    }

    public interface IConnectionWithInitString
    {
        ConnectionError Open(string initString, IValidator validator);
        ConnectionError Close();
    }
    public interface IByteReader
    {
        byte[] Read();
    }

    public interface IByteWriter
    {
        void Write(byte[] data);
    }

    public interface IStringReader
    {
        string Read();
    }

    public interface IStringWriter
    {
        void Write(string data);
    }
}
