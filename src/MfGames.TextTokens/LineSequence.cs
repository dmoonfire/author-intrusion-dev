// <copyright file="LineSequence.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens
{
    using System;

    /// <summary>
    /// A simple, low-overhead sequence for tokens.
    /// </summary>
    public struct LineSequence : IEquatable<LineSequence>
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
        public LineSequence(int id)
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
        public static bool operator ==(LineSequence left, LineSequence right)
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
        public static bool operator !=(LineSequence left, LineSequence right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// </summary>
        /// <param name="other">
        /// </param>
        /// <returns>
        /// </returns>
        public bool Equals(LineSequence other)
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

            return obj is LineSequence && this.Equals((LineSequence)obj);
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