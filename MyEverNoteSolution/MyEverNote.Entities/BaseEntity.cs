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
    public class BaseEntity
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required,DisplayName("Oluşturulan Tarih")]
        public DateTime CreatedOn { get; set; }


        [Required, DisplayName("Güncelleme Tarih")]
        public DateTime ModifiedOn { get; set; }


        [Required,DisplayName("Oluşturulan Kullanıcı"),StringLength(30)]
        public string ModifiedUserName { get; set; }

    }
}
