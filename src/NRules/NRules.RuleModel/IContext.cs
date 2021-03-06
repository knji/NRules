using System;

namespace NRules.RuleModel
{
    /// <summary>
    /// Rules engine execution context.
    /// Can be used by rules to interact with the rules engine, i.e. insert, update, retract facts.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Halts rules execution. The engine continues execution of the current rule and exits the execution cycle.
        /// </summary>
        void Halt();

        /// <summary>
        /// Inserts a new fact into the rules engine's memory.
        /// </summary>
        /// <param name="fact">New fact to insert.</param>
        void Insert(object fact);

        /// <summary>
        /// Updates existing fact in the rules engine's memory.
        /// </summary>
        /// <param name="fact">Existing fact to update.</param>
        void Update(object fact);

        /// <summary>
        /// Updates existing fact in the rules engine's memory.
        /// First the update action is applied to the fact, then the fact is updated in the engine's memory.
        /// </summary>
        /// <param name="fact">Existing fact to update.</param>
        /// <param name="updateAction">Action to apply to the fact.</param>
        void Update<T>(T fact, Action<T> updateAction);

        /// <summary>
        /// Removes existing fact from the rules engine's memory.
        /// </summary>
        /// <param name="fact">Existing fact to remove.</param>
        void Retract(object fact);
    }
}