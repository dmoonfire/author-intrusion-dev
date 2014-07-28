// <copyright file="IPersistenceFactory.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// 
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion.IO
{
    /// <summary>
    /// A persistence factory is a factory class that hooks up a protocol, such as "file://"
    /// or "ai://" to a specific instance of an IPersistence class.
    /// </summary>
    public interface IPersistenceFactory
    {
        /// <summary>
        /// Gets the name of the URI protocol that this factory handles, without the
        /// trailing "://". For example, "file" or "ai" instead of "file://" or "ai://".
        /// </summary>
        string Protocol { get; }

        /// <summary>
        /// Constructs an IPersistence object based on the given path. This will be the
        /// component of the path after the "://". For example, this method will get "a/b.xml"
        /// from "file://a/b.xml".
        /// </summary>
        /// <param name="path">The URI path component after the "://".</param>
        /// <returns>An IPersistence object representing the path.</returns>
        IPersistence CreatePersistence(string path);
    }
}