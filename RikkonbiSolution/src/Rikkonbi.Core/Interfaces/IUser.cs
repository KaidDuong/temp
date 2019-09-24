using System;

namespace Rikkonbi.Core.Interfaces
{
    /// <summary>
    /// Represents a user in the identity system
    /// </summary>
    /// <typeparam name="TKey">The type used for the primary key for the user.</typeparam>
    public interface IUser<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Gets or sets the primary key for this user.
        /// </summary>
        TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the user name for this user.
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets a salted and hashed representation of the password for this user.
        /// </summary>
        string PasswordHash { get; set; }

        /// <summary>
        /// A random value that must change whenever a users credentials change (password changed, login removed)
        /// </summary>
        string SecurityStamp { get; set; }

        /// <summary>
        /// Gets or sets the full name for this user.
        /// </summary>
        string FullName { get; set; }

        /// <summary>
        /// Gets or sets the avatar's url for this user.
        /// </summary>
        string Avatar { get; set; }

        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their email address.
        /// </summary>
        bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the user could be locked out.
        /// </summary>
        bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the number of failed login attempts for the current user.
        /// </summary>
        int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets the date and time, in UTC, when any user lockout ends.
        /// </summary>
        /// <remarks>A value in the past means the user is not locked out.</remarks> 
        DateTimeOffset? LockoutEnd { get; set; }
    }
}