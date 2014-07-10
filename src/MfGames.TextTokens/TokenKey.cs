// <copyright file="TokenKey.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens
{
    using System;

    /// <summary>
    /// A simple, low-overhead identifiers for tokens.
    /// </summary>
    public struct TokenKey : IEquatable<TokenKey>
    {
        #region Fields

        /// <summary>
        /// </summary>
        public int Id;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="id">
        /// </param>
        public TokenKey(int id)
        {
            this.Id = id;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="left">
        /// </param>
        /// <param name="right">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator ==(TokenKey left, TokenKey right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// </summary>
        /// <param name="left">
        /// </param>
        /// <param name="right">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator !=(TokenKey left, TokenKey right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// </summary>
        /// <param name="other">
        /// </param>
        /// <returns>
        /// </returns>
        public bool Equals(TokenKey other)
        {
            return this.Id == other.Id;
        }

        /// <summary>
        /// </summary>
        /// <param name="obj">
        /// </param>
        /// <returns>
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is TokenKey && this.Equals((TokenKey)obj);
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public override int GetHashCode()
        {
            return this.Id;
        }

        #endregion
    }
}