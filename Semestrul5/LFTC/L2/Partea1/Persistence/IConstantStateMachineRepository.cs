using Partea1.Model;

namespace Partea1.Persistence
{
    public interface IConstantStateMachineRepository
    {
        ConstantStateMachine Get(string fileName);
    }
}
