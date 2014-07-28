// <copyright file="FilePersistenceFactory.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    using System;

    /// <summary>
    /// Defines the persistence factory for the "file" protocol which interacts
    /// with the local file system.
    /// </summary>
    public class FilePersistenceFactory : IPersistenceFactory
    {
        #region Public Properties

        /// <summary>
        /// Gets the name of the URI protocol that this factory handles, without the
        /// trailing "://". For example, "file" or "ai" instead of "file://" or "ai://".
        /// </summary>
        public string Protocol
        {
            get
            {
                return "file";
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Constructs an IPersistence object based on the given path. This will be the
        /// component of the path after the "://". For example, this method will get "a/b.xml"
        /// from "file://a/b.xml".
        /// </summary>
        /// <param name="path">
        /// The URI path component after the "://".
        /// </param>
        /// <returns>
        /// An IPersistence object representing the path.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public IPersistence CreatePersistence(string path)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}