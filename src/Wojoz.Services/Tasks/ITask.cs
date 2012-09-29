namespace Wojoz.Services.Tasks
{
    /// <summary>
    /// Interface for defining an Tasks.
    /// </summary>
    public interface ITask
    {
        void Execute(object state);
    }
}
