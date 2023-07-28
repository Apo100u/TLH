using TLH.Gameplay.Interactions.Types;

namespace TLH.Gameplay.Interactions.Receivers
{
    public interface IInteractionReceiver<TInteraction> : IInteractionReceiver where TInteraction : Interaction
    {
        public void HandleInteraction(TInteraction interaction);
    }

    public interface IInteractionReceiver
    {
        // Non-generic version of this interface, so the generic one can inherit from it, making it possible to declare a collection that still
        // is able to hold all generic IInteractionReceiver implementations, like List<IInteractionReceiver>.
        
        // Unfortunately, this is rather workaround than a real solution, but at the time of writing I wasn't able to find anything better.
        // Option to make the generic parameter contravariant ("in") was considered, but this enables to use the interface with another inheriting type,
        // which is not what I was looking for - I don't want it to be possible.
        
        // Another "solution" would be to check in runtime for all types that inherit from the generic parameter and declare all possible collections in
        // runtime, but this looks like a lot of code that could add unnecessary complexity, while this workaround with additional empty interface is at least
        // simple and straightforward.
    }
}