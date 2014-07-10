// <copyright file="KeyGenerator.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens
{
    using System.Threading;

    /// <summary>
    /// </summary>
    public class KeyGenerator : IKeyGenerator
    {
        #region Fields

        /// <summary>
        /// </summary>
        private int nextId;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        static KeyGenerator()
        {
            Instance = new KeyGenerator();
        }

        /// <summary>
        /// </summary>
        public KeyGenerator()
        {
            this.nextId = 1;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public static IKeyGenerator Instance { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public LineKey GetNextLineKey()
        {
            int id = Interlocked.Increment(ref this.nextId);
            var lineKey = new LineKey(id);
            return lineKey;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public TokenKey GetNextTokenKey()
        {
            int id = Interlocked.Increment(ref this.nextId);
            var tokenKey = new TokenKey(id);
            return tokenKey;
        }

        #endregion
    }
}