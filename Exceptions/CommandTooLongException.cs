using System.IO;

namespace RetailWay.Integration.LibPCBS.Exceptions
{
    public sealed class CommandTooLongException : IOException
    {
        public CommandTooLongException() : base("Значение команды может содержать до 52 символов включительно.") { }
    }
}