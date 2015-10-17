namespace EventBus.Messaging
{
    /// <summary>
    /// Predefined types of methods.
    /// </summary>
    public enum MethodType
    {
        /// <summary>
        /// Get indicates that the sender wants some values from the target
        /// The response to a get should be an update with the requested values.
        /// </summary>
        Get,
        /// <summary>
        /// Updates indicates that this method contains updated values for the target.
        /// </summary>
        Update,
        /// <summary>
        /// Indicates that the sender has been started
        /// </summary>
        StatusStarted,
        /// <summary>
        /// Indicates that the sender has been stopped
        /// </summary>
        StatusStopped
    }
}