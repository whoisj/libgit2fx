namespace Libgit2
{
    public enum AttributeType
    {
        /// <summary>
        /// The attribute has been left unspecified
        /// </summary>
        Unspecified = 0,
        /// <summary>
        /// The attribute has been set
        /// </summary>
        True = 1,
        /// <summary>
        /// The attribute has been unset
        /// </summary>
        False = 2,
        /// <summary>
        /// This attribute has a value
        /// </summary> 
        Value = 3,
    }
}
