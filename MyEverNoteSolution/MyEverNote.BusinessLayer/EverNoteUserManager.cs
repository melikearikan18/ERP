using MyEverNote.Common.Helpers;
using MyEverNote.DataAccessLayer.EntityFramework;
using MyEverNote.Entities;
using MyEverNote.Entities.Messages;
using MyEverNote.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.BusinessLayer
{
    public class EverNoteUserManager
    {
        private Repository<EverNoteUser> repo_user = new Repository<EverNoteUser>();

        /*Yöntem-2*/

        //public EverNoteUser RegisterUser(RegisterViewModel data)
        //{
        //    EverNoteUser user = repo_user.Find(x => x.UserName == data.UserName || x.Email == data.Email);

        //    if (user!=null)
        //    {
        //        throw new Exception("Kayıtlı kullanıcı yada e-posta adresi.");
        //    }

        //    //return user;
        //}

        /*Yöntem-2-Son*/

        public BusinessLayerResult<EverNoteUser> RegisterUser(RegisterViewModel data)
        {
            EverNoteUser user = repo_user.Find(x => x.UserName == data.UserName || x.Email == data.Email);
            BusinessLayerResult<EverNoteUser> res = new BusinessLayerResult<EverNoteUser>();

            if (user!=null)
            {
                if (user.UserName==data.UserName)
                {
                    res.AddError(ErrorMessageCode.UserNameAlreadyExist, "Kullanıcı adı kayıtlı.");
                    
                }
                if (user.Email==data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "E-Posta kayıtlı.");
                    
                }
               
            }
            else
            {
                int dbResult = repo_user.Insert(new EverNoteUser() {
                    UserName=data.UserName,
                    Email=data.Email,
                    Password=data.Password,
                    ActivateGuid=Guid.NewGuid(),
                    IsActive=false,
                    IsAdmin=false
                });

                if (dbResult>0)
                {
                    res.Result = repo_user.Find(x => x.Email == data.Email && x.UserName == data.UserName);
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.UserName};\n hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'> tıklayınız</a>";
                    MailHelper.SendMail(body, res.Result.Email, "MyEverNote Hesap Aktifleştirme");
                }
            }
            return res;
        }

        public BusinessLayerResult<EverNoteUser> LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<EverNoteUser> res = new BusinessLayerResult<EverNoteUser>();

            res.Result = repo_user.Find(x => x.UserName == data.UserName && x.Password == data.Password);

            if (res.Result!=null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen E-Posta adresinizi kontrol ediniz.");

                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserNameOrPassWrong, "Kullanıcı adı veya şifre uyuşmuyor.");

            }
            return res;
        }

        public BusinessLayerResult<EverNoteUser> ActivateUser(Guid activeId)
        {
            BusinessLayerResult<EverNoteUser> res = new BusinessLayerResult<EverNoteUser>();

            res.Result = repo_user.Find(x => x.ActivateGuid == activeId);
            if (res.Result != null) 
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiş.");

                }
                res.Result.IsActive = true;
                repo_user.Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesExist, "Aktivasyon kodu hatalı.");
            }

            return res;
        }

        public BusinessLayerResult<EverNoteUser> GetUserById(int id)
        {
            BusinessLayerResult<EverNoteUser> res = new BusinessLayerResult<EverNoteUser>();
            res.Result = repo_user.Find(x => x.Id == id);
            if (res.Result == null) 
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı.");
            }
            return res;
        }
    }
}
