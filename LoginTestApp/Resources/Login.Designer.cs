﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoginTestApp.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Login {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Login() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LoginTestApp.Resources.Login", typeof(Login).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Por supuesto podemos ayudarte, por favor selecciona el tipo de ayuda que deseas..
        /// </summary>
        internal static string BodyText {
            get {
                return ResourceManager.GetString("BodyText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vamos a enviar una notificacion a tu direccion de correo registrada, asegurate de tener acceso a el..
        /// </summary>
        internal static string BodyTextFooter {
            get {
                return ResourceManager.GetString("BodyTextFooter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Olvido su contraseña?.
        /// </summary>
        internal static string ForgotPasswordText {
            get {
                return ResourceManager.GetString("ForgotPasswordText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Olvido su contraseña?.
        /// </summary>
        internal static string HeaderText {
            get {
                return ResourceManager.GetString("HeaderText", resourceCulture);
            }
        }
    }
}
