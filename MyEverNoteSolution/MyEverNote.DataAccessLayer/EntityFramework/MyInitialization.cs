using FakeData;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.DataAccessLayer.EntityFramework
{
    public class MyInitialization:CreateDatabaseIfNotExists<DatabaseContext>
    {
        // Adding Admin User
        protected override void Seed(DatabaseContext context) // db den context türettik
        {
            EverNoteUser admin = new EverNoteUser()
            {
                Name="Melike",
                Surname="Arıkan",
                Email="melike@gmail.com",
                ActivateGuid=Guid.NewGuid(),
                IsActive=true,
                IsAdmin=true,
                UserName="MelikeA",
                Password="12345",
                CreatedOn=DateTime.Now,
                ModifiedOn=DateTime.Now.AddMinutes(5),
                ModifiedUserName="MelikeA",
                ProfilImageFileName="userImage.jfif"

            };

            // Adding Standart User

            EverNoteUser standartUser = new EverNoteUser()
            {
                Name = "Muhammet",
                Surname = "Tiryaki",
                Email = "mtiryaji@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                UserName = "Mami",
                Password = "12345",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "MelikeA",
                ProfilImageFileName = "userImage.jfif"


            };

            context.EverNoteUsers.Add(admin);
            context.EverNoteUsers.Add(standartUser);





            // Using Fake User

            for (int i = 0; i < 8; i++)
            {
                EverNoteUser fake = new EverNoteUser()
                {
                    Name = NameData.GetFirstName(),
                    Surname = NameData.GetSurname(),
                    Email = NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = true,
                    UserName = $"user{i}",
                    Password = "123",
                    CreatedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                    ModifiedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                    ModifiedUserName = $"user{i}",
                    ProfilImageFileName = "userImage.jfif"


                };

                context.EverNoteUsers.Add(fake);
            }


            context.SaveChanges();

            // User List For Using

            List<EverNoteUser> userList = context.EverNoteUsers.ToList();

            // Adding Fake Categories

            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title=PlaceData.GetStreetName(),
                    Description=PlaceData.GetAddress(),
                    CreatedOn=DateTime.Now,
                    ModifiedOn=DateTime.Now,
                    ModifiedUserName="MelikeA"
                };

                context.Categories.Add(cat);

                // Adding Fake Notes

                for (int k = 0; k < NumberData.GetNumber(5,9); k++)
                {
                    EverNoteUser owner = userList[NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        Title=TextData.GetAlphabetical(NumberData.GetNumber(5,25)),
                        Text= TextData.GetSentences(NumberData.GetNumber(1, 3)),
                        Category=cat,
                        IsDraft=false,
                        LikeCount=NumberData.GetNumber(1,9),
                        Owner=owner,
                        CreatedOn= DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                        ModifiedOn= DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                        ModifiedUserName=owner.UserName
                    };
                    cat.Notes.Add(note);

                    //Adding Fake Comment

                    for (int j = 0; j < NumberData.GetNumber(3,5); j++)
                    {
                        EverNoteUser comment_owner = userList[NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment=new Comment()
                        {
                            Text=TextData.GetSentence(),
                            Owner=comment_owner,
                            CreatedOn= DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                            ModifiedOn= DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                            ModifiedUserName=comment_owner.UserName,
                            Note=note
                        };
                        note.Comments.Add(comment);

                        // Adding Fake Likes
                        for (int m = 0; m < note.LikeCount; m++)
                        {
                            Liked liked = new Liked()
                            {
                                LikedUser=userList[m]
                            };
                            note.Likes.Add(liked);
                        }
                    }

                }
                context.SaveChanges();
            }
        }
    }
}
