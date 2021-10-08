namespace Shoalace.Domain.Interfaces.Commands
{
    public interface IResultadoCommand
    {
        public bool Valid { get; }
        public bool Invalid { get; }
    }
}
