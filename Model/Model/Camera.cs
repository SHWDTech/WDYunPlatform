using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class Camera: SysModelBase, ICamera
    {
        [Display(Name = "摄像头外部ID")]
        [MaxLength(50)]
        public string CameraOutId { get; set; }

        [Display(Name = "摄像头所属域")]
        public SysDomain CameraDomain { get; set; }

        [Display(Name = "摄像头登录名")]
        [MaxLength(25)]
        public string AccessName { get; set; }

        [Display(Name = "摄像头登陆密码")]
        [MaxLength(50)]
        public string AccessPassword { get; set; }

        [Display(Name = "摄像头登录地址")]
        [MaxLength(200)]
        public string AccessUrl { get; set; }

        [Display(Name = "摄像头登录类型")]
        public int AccessType { get; set; }

        [Display(Name = "摄像头所属公司")]
        public string Compnany { get; set; }

        [Display(Name = "摄像头附加信息")]
        public string ExtraInformation { get; set; }
    }
}
