using System;
using System.Runtime.Serialization;

namespace SampleEntities{
    [Serializable]
    public class Customer : ISerializable{
        private int _privateIntField;
        protected int _protectedIntField;
        public int _publicIntField;

        public Customer(SerializationInfo info, StreamingContext context){
            _privateIntField = info.GetInt32("_privateIntField");
            _protectedIntField = info.GetInt32("_protectedIntField");
            _publicIntField = info.GetInt32("_publicIntField");
            ProtectedPropertyInt = info.GetInt32("ProtectedPropertyInt");
            PrivatePropertyInt = info.GetInt32("PrivatePropertyInt");
            PublicPropertyInt = info.GetInt32("PublicPropertyInt");
        }

        public Customer(){
            _protectedIntField = 1;
            _privateIntField = 2;
            _publicIntField = 3;
            ProtectedPropertyInt = 4;
            PrivatePropertyInt = 5;
            PublicPropertyInt = 6;
        }

        protected int ProtectedPropertyInt { get; set; }
        private int PrivatePropertyInt { get; set; }
        public int PublicPropertyInt { get; set; }

        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context){
            info.AddValue("_protectedIntField", _protectedIntField);
            info.AddValue("_privateIntField", _privateIntField);
            info.AddValue("_publicIntField", _publicIntField);
            info.AddValue("ProtectedPropertyInt", ProtectedPropertyInt);
            info.AddValue("PrivatePropertyInt", PrivatePropertyInt);
            info.AddValue("PublicPropertyInt", PublicPropertyInt);
        }

        #endregion

        public void DoIt(){
            int field = _privateIntField;
            _privateIntField = field;
        }
    }
}