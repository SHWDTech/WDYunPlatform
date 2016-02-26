namespace SHWDTech.Platform.Common.Enum
{
    public enum ModelState
    {
        /// <summary>
        /// Entity is unchanged
        /// </summary>
        Unchanged = 0,

        /// <summary>
        /// Entity is new
        /// </summary>
        Added = 1,

        /// <summary>
        /// Entity has been modified
        /// </summary>
        Changed = 2,

        /// <summary>
        /// Entity has been deleted
        /// </summary>
        Deleted = 3
    }
}
