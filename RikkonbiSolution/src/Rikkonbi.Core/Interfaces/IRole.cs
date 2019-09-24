using System;

namespace Rikkonbi.Core.Interfaces
{
    public interface IRole<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the primary key for this role.
        /// </summary>
        TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description for this role.
        /// </summary>
        string Description { get; set; }
    }
}