using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 摄像头
    /// </summary>
    [Serializable]
    public class Camera : SysDomainModelBase, ICamera
    {
        [Display(Name = "摄像头外部ID")]
        [MaxLength(50)]
        public virtual string CameraOutId { get; set; }

        [Display(Name = "摄像头登录名")]
        [MaxLength(25)]
        public virtual string AccessName { get; set; }

        [Display(Name = "摄像头登陆密码")]
        [MaxLength(50)]
        public virtual string AccessPassword { get; set; }

        [Display(Name = "摄像头登录地址")]
        [MaxLength(200)]
        public virtual string AccessUrl { get; set; }

        [Display(Name = "摄像头登陆地址端口号")]
        public virtual int AccessPort { get; set; }

        [Display(Name = "摄像头登录类型")]
        public virtual SysDictionary AccessType { get; set; }

        [Display(Name = "摄像头所属公司")]
        public virtual string Compnany { get; set; }

        [Display(Name = "摄像头附加信息")]
        public virtual string ExtraInformation { get; set; }

        [Required]
        [Display(Name = "摄像头状态")]
        public virtual CameraStatus CameraStatus { get; set; }
    }
}