using Matrix.Extension;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Matrix.Agent.Log")]
[assembly: AssemblyDescription("Matrix Platform Logger Agent")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Matrix")]
[assembly: AssemblyCopyright("Copyright © paramg.com Paramesh Gunasekaran 2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("8718aad9-96b8-40d5-b1f1-ed92e2225267")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyMetadata("Name", "Logger")]

public static class AssemblyInfo
{
    public static Guid Id
    {
        get
        {
            var result = Guid.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            var attributes = assembly.GetCustomAttributes(typeof(GuidAttribute), true);

            if (attributes.Length > 0)
                result = Guid.Parse(((GuidAttribute)attributes[0]).Value);

            return result;
        }
    }

    public static string Name { get { return AssemblyMetadata("Name"); } }

    public static string Description
    {
        get
        {
            var result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            var attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), true);

            if (attributes.Length > 0)
                result = ((AssemblyDescriptionAttribute)attributes[0]).Description;

            return result;
        }
    }

    public static string Version
    {
        get
        {
            var result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            result = fvi.FileVersion;

            return result;
        }
    }

    public static string Build
    {
        get
        {
            var result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            result = new FileInfo(assembly.Location).LastWriteTime.ToString();

            return result;
        }
    }

    public static string Copyright
    {
        get
        {
            var result = string.Empty;

            var assembly = Assembly.GetExecutingAssembly();

            var attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);

            if (attributes.Length > 0)
                result = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

            return result;
        }
    }

    public static string Hash
    {
        get
        {
            var result = string.Empty;

            result = File.ReadAllBytes(Assembly.GetExecutingAssembly().Location).SHA256();

            return result;
        }
    }

    private static string AssemblyMetadata(string key)
    {
        var result = string.Empty;

        var assembly = Assembly.GetExecutingAssembly();

        var attributes = assembly.GetCustomAttributes(typeof(AssemblyMetadataAttribute), true);

        if (attributes.Length > 0)
        {
            foreach (var i in attributes)
            {
                var meta = i as AssemblyMetadataAttribute;

                if (meta != null && meta.Key.Equals(key))
                    result = meta.Value;
            }
        }

        return result;
    }
}