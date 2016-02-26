using System;
using Newtonsoft.Json;
using SHWDTech.Platform.Common.Component;
using SHWDTech.Platform.Common.Enum;

namespace SHWDTech.Model.Model.ModelBase
{
    [Serializable]
    public abstract class ModelBase
    {
        [JsonIgnore]
        private bool _deleteSubmited;

        [JsonIgnore] private object _tag;

        [JsonIgnore]
        public abstract  ModelState State { get; set; }

        [JsonIgnore]
        public virtual bool IsDeleted => State == ModelState.Deleted;

        [JsonIgnore]
        public virtual bool IsNew => State == ModelState.Added;

        public virtual void MarkToDelete()
        {
            if (State != ModelState.Added)
            {
                State = ModelState.Deleted;
            }
        }

        [JsonIgnore]
        public virtual bool DeleteSubmited
        {
            get
            {
                return _deleteSubmited;
            }
            set
            {
                if (IsDeleted)
                    _deleteSubmited = value;
                else
                {
                    throw WdtException.Error("对象并没有标记为删除，不能设置DeleteSubmited = true。");
                }
            }
        }

        [JsonIgnore]
        public virtual object Tag
        {
            get { return _tag; }
            set
            {
                if(Equals(_tag, value)) return;

                _tag = value;
            }
        }

        public virtual void RemoveDeleteMark()
        {
            if (DeleteSubmited)
                throw WdtException.Error("对象已删除，不能一处删除表示");

            if (State != ModelState.Added)
            {
                State = ModelState.Changed;
            }
        }

        public virtual void AcceptChanges()
        {
            if (DeleteSubmited)
                throw WdtException.Error("对象已删除，不应调用对象的AcceptChanges方法");

            State = ModelState.Unchanged;
        }
    }
}
