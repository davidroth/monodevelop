// 
// BrandingService.cs
//  
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
// 
// Copyright (c) 2011 Xamarin Inc. (http://xamarin.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Xml;
using System.Reflection;

namespace MonoDevelop.Core
{
	/// <summary>
	/// Branding service do access monodevelop branding data file.
	/// </summary>
	public static class BrandingService
	{
		public static readonly XmlDocument BrandingDocument;
		public static readonly string ApplicationName;
		
		static BrandingService ()
		{
			XmlDocument brandingData = null;
			try {
				using (var stream = Assembly.GetEntryAssembly ().GetManifestResourceStream ("BrandingData.xml")) {
					if (stream != null) {
						brandingData = new XmlDocument ();
						brandingData.Load (stream);
					}
				}
			} catch (Exception) {
				brandingData = null;
			}
			
			// may happen in mdtool.
			if (brandingData == null) {
				ApplicationName = "MonoDevelop";
				return;
			}
			
			BrandingDocument = brandingData;
			try {
				ApplicationName = brandingData.DocumentElement ["ApplicationName"].GetAttribute ("value");
			} catch (Exception e) {
				LoggingService.LogError ("Error while reading application name from branding xml.", e);
				ApplicationName = "Unknown";
			}
		}
	}
}

