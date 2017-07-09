using System;

namespace TaskManagement.Common
{
    /// <summary>
    ///     Exception thrown when a required child of the primary object is not found.
    /// </summary>
    [Serializable]
    public class ChildObjectNotFoundException : Exception
    {
        public ChildObjectNotFoundException(string message) : base(message)
        {
        }
    }
}