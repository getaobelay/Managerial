﻿using Application.Interfaces;
using System;

namespace Application.ViewModels
{
    public class BaseViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}