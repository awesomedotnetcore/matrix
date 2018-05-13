using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Serialization;

namespace Matrix.Web.Services
{
    public class WebServiceInvoker
    {
        #region Events

        public event EventHandler Started;

        public event EventHandler Completed;

        public event EventHandler Error;

        #endregion Events

        #region Fields

        private Assembly _assembly;

        private List<string> _services;

        private Dictionary<string, Type> _types;

        private bool _processing;

        #endregion Fields

        #region Properties

        public bool Processing
        {
            get { return _processing; }

            private set
            {
                try
                {
                    if (value)
                    {
                        if (Started != null)
                            Started(this, new EventArgs());
                    }

                    if (!value)
                    {
                        if (Completed != null)
                            Completed(this, new EventArgs());
                    }

                    _processing = value;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        #endregion Properties

        #region .ctor

        public WebServiceInvoker(Uri uri)
        {
            _services = new List<string>();

            _types = new Dictionary<string, Type>();

            _assembly = BuildAssembly(uri);

            Type[] types = _assembly.GetExportedTypes();

            foreach (Type type in types)
            {
                _services.Add(type.FullName);
                _types.Add(type.FullName, type);
            }
        }

        #endregion .ctor

        #region Public Methods

        public List<string> GetServiceMethods(string service)
        {
            List<string> methods = new List<string>();

            if (!_types.ContainsKey(service))
                throw new Exception("Service not available");
            else
            {
                Type type = _types[service];

                // only find methods of this object type (the one we generated)
                // we don't want inherited members (this type inherited from SoapHttpClientProtocol)
                foreach (MethodInfo minfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
                    methods.Add(minfo.Name);

                return methods;
            }
        }

        public T InvokeMethod<T>(string service, string method, params object[] args)
        {
            T result = default(T);

            Processing = true;

            object o = _assembly.CreateInstance(service);

            o = BeforeInvokeMethod(o);

            Type t = o.GetType();

            result = (T)t.InvokeMember(method, BindingFlags.InvokeMethod, null, o, args);

            Processing = false;

            return result;
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual object BeforeInvokeMethod(object o)
        {
            return o;
        }

        #endregion Protected Methods

        #region Private Helpers

        private ServiceDescriptionImporter BuildProxy(XmlTextReader reader)
        {
            if (!ServiceDescription.CanRead(reader))
                throw new Exception("Invalid webservice description");

            ServiceDescription wsdl = ServiceDescription.Read(reader);

            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
            importer.ProtocolName = "Soap";
            importer.AddServiceDescription(wsdl, null, null);
            importer.Style = ServiceDescriptionImportStyle.Client;
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;

            return importer;
        }

        private Assembly CompileAssembly(ServiceDescriptionImporter importer)
        {
            CompilerResults result = null;

            CodeNamespace ns = new CodeNamespace();

            CodeCompileUnit code = new CodeCompileUnit();

            code.Namespaces.Add(ns);

            ServiceDescriptionImportWarnings warnings = importer.Import(ns, code);

            if (warnings == 0)
            {
                CodeDomProvider compiler = CodeDomProvider.CreateProvider("CSharp");

                string[] references = new string[2] { "System.Web.Services.dll", "System.Xml.dll" };

                CompilerParameters parameters = new CompilerParameters(references);

                result = compiler.CompileAssemblyFromDom(parameters, code);

                foreach (CompilerError oops in result.Errors)
                    throw new Exception("Compilation error creating assembly");
            }
            else
                throw new Exception("Invalid WSDL");

            return result.CompiledAssembly;
        }

        private Assembly BuildAssembly(Uri uri)
        {
            if (String.IsNullOrEmpty(uri.ToString()))
                throw new Exception("Webservice not found");

            XmlTextReader reader = new XmlTextReader(uri.ToString() + "?wsdl");

            ServiceDescriptionImporter descriptionImporter = BuildProxy(reader);

            return CompileAssembly(descriptionImporter);
        }

        #endregion Private Helpers
    }
}