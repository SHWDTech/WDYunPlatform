﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JingAnWebService.InsertDeviceBaseInfo {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://standbaseinfo.web.ths.com/", ConfigurationName="InsertDeviceBaseInfo.IInsertDeviceBaseInfo")]
    public interface IInsertDeviceBaseInfo {
        
        // CODEGEN: Generating message contract since element name jsons from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoResponse insertDeviceBaseInfo(JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfo request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoResponse> insertDeviceBaseInfoAsync(JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfo request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class insertDeviceBaseInfo {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="insertDeviceBaseInfo", Namespace="http://standbaseinfo.web.ths.com/", Order=0)]
        public JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoBody Body;
        
        public insertDeviceBaseInfo() {
        }
        
        public insertDeviceBaseInfo(JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class insertDeviceBaseInfoBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string jsons;
        
        public insertDeviceBaseInfoBody() {
        }
        
        public insertDeviceBaseInfoBody(string jsons) {
            this.jsons = jsons;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class insertDeviceBaseInfoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="insertDeviceBaseInfoResponse", Namespace="http://standbaseinfo.web.ths.com/", Order=0)]
        public JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoResponseBody Body;
        
        public insertDeviceBaseInfoResponse() {
        }
        
        public insertDeviceBaseInfoResponse(JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class insertDeviceBaseInfoResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public insertDeviceBaseInfoResponseBody() {
        }
        
        public insertDeviceBaseInfoResponseBody(string @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IInsertDeviceBaseInfoChannel : JingAnWebService.InsertDeviceBaseInfo.IInsertDeviceBaseInfo, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class InsertDeviceBaseInfoClient : System.ServiceModel.ClientBase<JingAnWebService.InsertDeviceBaseInfo.IInsertDeviceBaseInfo>, JingAnWebService.InsertDeviceBaseInfo.IInsertDeviceBaseInfo {
        
        public InsertDeviceBaseInfoClient() {
        }
        
        public InsertDeviceBaseInfoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public InsertDeviceBaseInfoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InsertDeviceBaseInfoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InsertDeviceBaseInfoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoResponse JingAnWebService.InsertDeviceBaseInfo.IInsertDeviceBaseInfo.insertDeviceBaseInfo(JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfo request) {
            return base.Channel.insertDeviceBaseInfo(request);
        }
        
        public string insertDeviceBaseInfo(string jsons) {
            JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfo inValue = new JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfo();
            inValue.Body = new JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoBody();
            inValue.Body.jsons = jsons;
            JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoResponse retVal = ((JingAnWebService.InsertDeviceBaseInfo.IInsertDeviceBaseInfo)(this)).insertDeviceBaseInfo(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoResponse> JingAnWebService.InsertDeviceBaseInfo.IInsertDeviceBaseInfo.insertDeviceBaseInfoAsync(JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfo request) {
            return base.Channel.insertDeviceBaseInfoAsync(request);
        }
        
        public System.Threading.Tasks.Task<JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoResponse> insertDeviceBaseInfoAsync(string jsons) {
            JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfo inValue = new JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfo();
            inValue.Body = new JingAnWebService.InsertDeviceBaseInfo.insertDeviceBaseInfoBody();
            inValue.Body.jsons = jsons;
            return ((JingAnWebService.InsertDeviceBaseInfo.IInsertDeviceBaseInfo)(this)).insertDeviceBaseInfoAsync(inValue);
        }
    }
}
