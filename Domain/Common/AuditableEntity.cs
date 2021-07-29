﻿using System;
using System.ComponentModel.DataAnnotations;
using Domain.Interfaces;

namespace Domain.Common
{
    public class AuditableEntity : IAuditableEntity
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string CreatedBy { get; set; }

        [MaxLength(256)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
