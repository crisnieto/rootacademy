﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rootAcademy.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.CursosWebServiceSoap")]
    public interface CursosWebServiceSoap {
        
        // CODEGEN: Generating message contract since element name conseguirCursosActualesResult from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/conseguirCursosActuales", ReplyAction="*")]
        rootAcademy.ServiceReference1.conseguirCursosActualesResponse conseguirCursosActuales(rootAcademy.ServiceReference1.conseguirCursosActualesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/conseguirCursosActuales", ReplyAction="*")]
        System.Threading.Tasks.Task<rootAcademy.ServiceReference1.conseguirCursosActualesResponse> conseguirCursosActualesAsync(rootAcademy.ServiceReference1.conseguirCursosActualesRequest request);
        
        // CODEGEN: Generating message contract since element name nombre from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/buscarCurso", ReplyAction="*")]
        rootAcademy.ServiceReference1.buscarCursoResponse buscarCurso(rootAcademy.ServiceReference1.buscarCursoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/buscarCurso", ReplyAction="*")]
        System.Threading.Tasks.Task<rootAcademy.ServiceReference1.buscarCursoResponse> buscarCursoAsync(rootAcademy.ServiceReference1.buscarCursoRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class conseguirCursosActualesRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="conseguirCursosActuales", Namespace="http://tempuri.org/", Order=0)]
        public rootAcademy.ServiceReference1.conseguirCursosActualesRequestBody Body;
        
        public conseguirCursosActualesRequest() {
        }
        
        public conseguirCursosActualesRequest(rootAcademy.ServiceReference1.conseguirCursosActualesRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class conseguirCursosActualesRequestBody {
        
        public conseguirCursosActualesRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class conseguirCursosActualesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="conseguirCursosActualesResponse", Namespace="http://tempuri.org/", Order=0)]
        public rootAcademy.ServiceReference1.conseguirCursosActualesResponseBody Body;
        
        public conseguirCursosActualesResponse() {
        }
        
        public conseguirCursosActualesResponse(rootAcademy.ServiceReference1.conseguirCursosActualesResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class conseguirCursosActualesResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement conseguirCursosActualesResult;
        
        public conseguirCursosActualesResponseBody() {
        }
        
        public conseguirCursosActualesResponseBody(System.Xml.Linq.XElement conseguirCursosActualesResult) {
            this.conseguirCursosActualesResult = conseguirCursosActualesResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class buscarCursoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="buscarCurso", Namespace="http://tempuri.org/", Order=0)]
        public rootAcademy.ServiceReference1.buscarCursoRequestBody Body;
        
        public buscarCursoRequest() {
        }
        
        public buscarCursoRequest(rootAcademy.ServiceReference1.buscarCursoRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class buscarCursoRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string nombre;
        
        public buscarCursoRequestBody() {
        }
        
        public buscarCursoRequestBody(string nombre) {
            this.nombre = nombre;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class buscarCursoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="buscarCursoResponse", Namespace="http://tempuri.org/", Order=0)]
        public rootAcademy.ServiceReference1.buscarCursoResponseBody Body;
        
        public buscarCursoResponse() {
        }
        
        public buscarCursoResponse(rootAcademy.ServiceReference1.buscarCursoResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class buscarCursoResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement buscarCursoResult;
        
        public buscarCursoResponseBody() {
        }
        
        public buscarCursoResponseBody(System.Xml.Linq.XElement buscarCursoResult) {
            this.buscarCursoResult = buscarCursoResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CursosWebServiceSoapChannel : rootAcademy.ServiceReference1.CursosWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CursosWebServiceSoapClient : System.ServiceModel.ClientBase<rootAcademy.ServiceReference1.CursosWebServiceSoap>, rootAcademy.ServiceReference1.CursosWebServiceSoap {
        
        public CursosWebServiceSoapClient() {
        }
        
        public CursosWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CursosWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CursosWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CursosWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        rootAcademy.ServiceReference1.conseguirCursosActualesResponse rootAcademy.ServiceReference1.CursosWebServiceSoap.conseguirCursosActuales(rootAcademy.ServiceReference1.conseguirCursosActualesRequest request) {
            return base.Channel.conseguirCursosActuales(request);
        }
        
        public System.Xml.Linq.XElement conseguirCursosActuales() {
            rootAcademy.ServiceReference1.conseguirCursosActualesRequest inValue = new rootAcademy.ServiceReference1.conseguirCursosActualesRequest();
            inValue.Body = new rootAcademy.ServiceReference1.conseguirCursosActualesRequestBody();
            rootAcademy.ServiceReference1.conseguirCursosActualesResponse retVal = ((rootAcademy.ServiceReference1.CursosWebServiceSoap)(this)).conseguirCursosActuales(inValue);
            return retVal.Body.conseguirCursosActualesResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<rootAcademy.ServiceReference1.conseguirCursosActualesResponse> rootAcademy.ServiceReference1.CursosWebServiceSoap.conseguirCursosActualesAsync(rootAcademy.ServiceReference1.conseguirCursosActualesRequest request) {
            return base.Channel.conseguirCursosActualesAsync(request);
        }
        
        public System.Threading.Tasks.Task<rootAcademy.ServiceReference1.conseguirCursosActualesResponse> conseguirCursosActualesAsync() {
            rootAcademy.ServiceReference1.conseguirCursosActualesRequest inValue = new rootAcademy.ServiceReference1.conseguirCursosActualesRequest();
            inValue.Body = new rootAcademy.ServiceReference1.conseguirCursosActualesRequestBody();
            return ((rootAcademy.ServiceReference1.CursosWebServiceSoap)(this)).conseguirCursosActualesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        rootAcademy.ServiceReference1.buscarCursoResponse rootAcademy.ServiceReference1.CursosWebServiceSoap.buscarCurso(rootAcademy.ServiceReference1.buscarCursoRequest request) {
            return base.Channel.buscarCurso(request);
        }
        
        public System.Xml.Linq.XElement buscarCurso(string nombre) {
            rootAcademy.ServiceReference1.buscarCursoRequest inValue = new rootAcademy.ServiceReference1.buscarCursoRequest();
            inValue.Body = new rootAcademy.ServiceReference1.buscarCursoRequestBody();
            inValue.Body.nombre = nombre;
            rootAcademy.ServiceReference1.buscarCursoResponse retVal = ((rootAcademy.ServiceReference1.CursosWebServiceSoap)(this)).buscarCurso(inValue);
            return retVal.Body.buscarCursoResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<rootAcademy.ServiceReference1.buscarCursoResponse> rootAcademy.ServiceReference1.CursosWebServiceSoap.buscarCursoAsync(rootAcademy.ServiceReference1.buscarCursoRequest request) {
            return base.Channel.buscarCursoAsync(request);
        }
        
        public System.Threading.Tasks.Task<rootAcademy.ServiceReference1.buscarCursoResponse> buscarCursoAsync(string nombre) {
            rootAcademy.ServiceReference1.buscarCursoRequest inValue = new rootAcademy.ServiceReference1.buscarCursoRequest();
            inValue.Body = new rootAcademy.ServiceReference1.buscarCursoRequestBody();
            inValue.Body.nombre = nombre;
            return ((rootAcademy.ServiceReference1.CursosWebServiceSoap)(this)).buscarCursoAsync(inValue);
        }
    }
}
