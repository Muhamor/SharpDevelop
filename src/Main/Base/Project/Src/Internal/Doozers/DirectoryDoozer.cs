﻿// <file>
//     <copyright see="prj:///doc/copyright.txt">2002-2005 AlphaSierraPapa</copyright>
//     <license see="prj:///doc/license.txt">GNU General Public License</license>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections;
using System.IO;

namespace ICSharpCode.Core
{
	/// <summary>
	/// Creates path names using a relative to the folder containing the addin file.
	/// </summary>
	/// <attribute name="path">
	/// Path relative to the directory which contains the .addin file defining the codon.
	/// </attribute>
	/// <returns>
	/// A string containing the full path name.
	/// </returns>
	public class DirectoryDoozer : IDoozer
	{
		public bool HandleConditions { get { return false; } }
		
		public object BuildItem(object caller, Codon codon, ArrayList subItems)
		{
			return Path.Combine(Path.GetDirectoryName(codon.AddIn.FileName), codon.Properties["path"]);
		}
	}
}
