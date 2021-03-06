﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Entities
{
    [Table("EvetNoteUsers")]
    public class EverNoteUser:BaseEntity
    {
        [StringLength(25),DisplayName("Ad")]
        public string Name { get; set; }

        [StringLength(25), DisplayName("Soyad")]
        public string Surname { get; set; }

        [StringLength(25),Required, DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [StringLength(150), Required, DisplayName("E-Posta")]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        public string Password { get; set; }
        [StringLength(30)]  // user12.jpeg

        public string ProfilImageFileName { get; set; }


        [DisplayName("Aktif mi?")]
        public bool IsActive { get; set; }

        [DisplayName("Yönetici mi?")]
        public bool IsAdmin { get; set; }

        [DisplayName("Benzersiz Id")]
        public Guid ActivateGuid { get; set; }

        public virtual List<Note> Notes { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Liked> Likes { get; set; }



    }
}
