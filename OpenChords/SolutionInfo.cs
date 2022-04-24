using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyDescription("OpenChords is a guitar chord management tool. It allows the user to present songs with guitar chords to a computer screen, so printing pages and pages of guitar chords is a thing of the past. Its opensource, easy to use, clean, and has a clutter-free interface.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("OpenChords")]
[assembly: AssemblyCopyright("GNU General Public License version 3.0 (GPLv3)")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

// The assembly version has following format :
//
// Major.Minor.Build.Revision
//
// You can specify all the values or you can use the default the Revision and 
// Build Numbers by using the '*' as shown below:
[assembly: AssemblyVersion("2.4.8.0")]

[assembly: log4net.Config.XmlConfigurator(Watch = true)]