﻿// ReSharper disable InconsistentNaming
namespace DreamFactory.Model.File
{
    using System.Collections.Generic;

    /// <summary>
    /// FolderRequest.
    /// </summary>
    public class FolderRequest
    {
        /// <summary>
        /// Gets Identifier/Name for the folder, localized to requested resource.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Full path of the folder, from the service including container.
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// An array of sub-folders to create.
        /// </summary>
        public List<FolderRequest> folder { get; set; }

        /// <summary>
        /// An array of files to create.
        /// </summary>
        public List<FileRequest> file { get; set; }
    }
}