using MyEverNote.DataAccessLayer;
using MyEverNote.DataAccessLayer.EntityFramework;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.BusinessLayer
{
    public class Test
    {
        //private Repository<Category> repo_category = new Repository<Category>();

        //private Repository<EverNoteUser> repo_user = new Repository<EverNoteUser>();
        //public Test()
        //{
        //    //DatabaseContext db = new DatabaseContext();
        //    ////db.Database.CreateIfNotExists();// Db oluşturulmamışsa
        //    //db.Categories.ToList();

        //    List<Category> categories = repo_category.List();
        //}

        //public void InsertTest()
        //{
        //    int result = repo_user.Insert(new EverNoteUser()
        //    {
        //        Name = "Melike",
        //        Surname = "Arıkan",
        //        Email="melikearikan18@gmail.com",
        //        ActivateGuid=Guid.NewGuid(),
        //        IsActive=true,
        //        IsAdmin=false,
        //        UserName="aaa",
        //        Password="123",
        //        CreatedOn=DateTime.Now,
        //        ModifiedOn=DateTime.Now.AddMinutes(5),
        //        ModifiedUserName="melikea"

        //    });
        //}
       

        //public void UpdateTest()
        //{
        //    EverNoteUser user = repo_user.Find(x => x.Name == "Melike");
        //    if (user!=null)
        //    {
        //        user.UserName = "melikeee";
        //        repo_user.Update(user);

        //    }
           
        //}

        //public void DeleteTest()
        //{
        //    EverNoteUser user = repo_user.Find(x => x.UserName == "aaa");

        //    if (user != null)
        //    {
        //        repo_user.Delete(user);
        //    }
        //}
    }
}
