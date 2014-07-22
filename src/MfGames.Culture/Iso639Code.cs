// <copyright file="Iso639Code.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Culture
{
    /// <summary>
    /// An immutable identifier for an ISO language code including support unknown
    /// languages to the <c>System.Globalization.CultureInfo</c> implementation. This
    /// provides values for all components of ISO 639 code, but primarily focuses on
    /// the ISO 639-3 implementation.
    /// </summary>
    public class Iso639Code
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this language is private use.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is private use; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivateUse { get; private set; }

        /// <summary>
        /// Gets the ISO 639-3 code for the language.
        /// </summary>
        /// <value>
        /// The three character language code.
        /// </value>
        public string LanguageCode3 { get; private set; }

        #endregion
    }
}